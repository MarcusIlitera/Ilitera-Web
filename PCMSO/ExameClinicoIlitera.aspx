
<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"   CodeBehind="ExameClinicoIlitera.aspx.cs" Inherits="Ilitera.Net.ExameClinicoIlitera" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

<script type="text/javascript">

function on_column_gettext(column, item, cellValue)
{
    //if (cellValue == 1)
    //    return "Sim";
    //else
    //	return "Nao";
    return cellValue;
}

function on_begin_edit(cell)
{
    //Get the current cell value
    var v = 0;
    var valor = cell.getValue();
    
    if (valor == "Sim/Positivo")
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

function on_end_edit(cell)
{
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
        return "Sim/Positivo";
    else
        return "Não/Negativo";
}

</script>

    <style type="text/css">
        .defaultFont
    {
    }
        .style7
        {
            width: 110px;
        }
        .style8
        {
            width: 17px;
        }
        .style9
        {
            width: 131px;
        }
        .style10
        {
            width: 119px;
        }
        .style11
        {
            width: 8px;
        }
        .inputBox
        {}
        .style15
        {
            width: 155px;
        }
        .style16
        {
            width: 359px;
        }
        .style17
        {
            width: 223px; 
        }
    .style18
    {
        width: 156px;
    }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
	
        
      <TABLE class="defaultFont" id="Table2" cellSpacing="0" cellPadding="0" align="center"
				border="0">
				<TR>
					<TD align="center">
                        <table ID="Table1" align="center" border="0" cellpadding="0" cellspacing="0" 
                            class="defaultFont">
                            <caption>
                                <asp:Label ID="lblNome" runat="server" Font-Bold="True" SkinID="TitleFont"> 
                                Exame Clínico</asp:Label>
                                <tr>
                                    <td class="style18">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" class="style18">
                                        <asp:Label ID="Label27" runat="server" SkinID="BoldFont">Resultado do Exame</asp:Label>
                                        <br>
                                        <asp:RadioButtonList ID="rblResultado" runat="server" BorderStyle="None" 
                                            BorderWidth="0px" CssClass="defaultFont" Height="24px" RepeatColumns="3" 
                                            RepeatLayout="Flow" tabIndex="1" Width="281px">
                                            <asp:ListItem Value="1">Apto</asp:ListItem>
                                            <asp:ListItem Value="2">Inapto</asp:ListItem>
                                            <asp:ListItem Value="3">Em Espera</asp:ListItem>
                                        </asp:RadioButtonList>

                                        <br />
                                        <br />

                                        <asp:Label ID="lblData" runat="server" SkinID="BoldFont">Data do Exame</asp:Label>
                                        <br>
                                        <asp:TextBox ID="wdtDataExame" runat="server" MaxLength="10" Width="90px"></asp:TextBox>
                                        <br>
                                        <asp:Label ID="lbl_Demissao2" runat="server" Font-Size="XX-Small" ForeColor="#CC0000" Text="Data Demissão:" Visible="False"></asp:Label>
                                        &nbsp;<asp:Label ID="lblDemissao" runat="server" Font-Size="XX-Small" ForeColor="#CC0000" Visible="False"></asp:Label>
                                        <br />
                                       

                                      
                            
                                    </td>
                                    <td align="center">
                                       
                                        <asp:Label ID="lblTipo" runat="server" SkinID="BoldFont">Tipo do Exame</asp:Label>
                                        &nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlTipoExame" runat="server" tabIndex="1" 
                                            Width="140px" AutoPostBack="True" Font-Size="X-Small" 
                                            onselectedindexchanged="ddlTipoExame_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chk_PacienteCritico" runat="server" AutoPostBack="True" CssClass="boldFont" OnCheckedChanged="Chk_PacienteCritico_CheckedChanged" Text="Paciente Crítico" Visible="False" />
                                        <br />
                                        <asp:Label ID="lblClinica" runat="server">Clínica</asp:Label>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlClinica" runat="server" AutoPostBack="True" 
                                            Font-Size="X-Small" Height="16px" 
                                            onselectedindexchanged="ddlClinica_SelectedIndexChanged" Width="268px">
                                        </asp:DropDownList>
                                        <br />
                                        &nbsp;<br>
                                        <asp:Label ID="lblMedico" runat="server">Médico</asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlMedico" runat="server" Font-Size="X-Small" 
                                            Height="16px" Width="268px">
                                        </asp:DropDownList>
&nbsp;                                        
                                   

                                    </td>
                                    <td>
                                    </td>
                                    <td align="center">
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <br />
                        <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Anamnese">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Exame Físico">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Indicadores Morbidade">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anotações / Obs.">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Exames Complementares">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Digitalização">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Testes Especiais">
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
                        <div style="BORDER-RIGHT:#b0b0b0 1px solid; BORDER-LEFT:#b0b0b0 1px solid; BORDER-BOTTOM:#b0b0b0 1px solid; padding: 5px 5px 5px 5px; width:838px;">
                            <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="838">
                                <eo:PageView ID="Pageview1" runat="server">
                                    <table ID="Table5" align="left" border="0" cellpadding="0" cellspacing="0" 
                                        class="defaultFont" width="750">
                                        <tr>
                                            <td>

                                                <table ID="Table9" border="0" cellpadding="0" cellspacing="0" 
                                                    class="defaultFont"  style="border-collapse: collapse">   

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblQueixas" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Você está bem de saúde ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbQueixaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbQueixaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small" />
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblAfastado" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Fica gripado com frequência ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbGripadoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbGripadoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10" >
                                                        </td>
                                                        <td class="style17"  align="right">
                                                            <asp:Label ID="lblBronquite" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Bronquite, asma, rinite ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbBronquiteS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbBronquiteN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label3" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Escuta bem ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbEscutaS" runat="server" CssClass="defaultFont" 
                                                               Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbEscutaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                         </td>

                                                    </tr>

                                                   
                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblDoenca" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Doenças Digestivas</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDigestivaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDigestivaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblMedicamentos" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Tem dores nas costas ou coluna ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDoresCostaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDoresCostaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>


                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblCirurgia" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Doenças do estômago ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbEstomagoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbEstomagoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblTabagista" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Tem reumatismo ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbReumatismoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbReumatismoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblTrauma" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Enxerga bem ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbEnxergaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbEnxergaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblEtilista" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Já esteve hospitalizado alguma vez?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbCirurgiaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbCirurgiaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblDeficiencia" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Tem dor de cabeça ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDorCabecaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDorCabecaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblEtilista0" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Tem alergia ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAlergiaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAlergiaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label4" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Já desmaiou alguma vez ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDesmaioS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDesmaioN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label5" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Fuma ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbTabagismoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbTabagismoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">                                                        
                                                            <asp:Label ID="Label8" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Já teve alguma fratura ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbTraumatismoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbTraumatismoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label10" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Bebe ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAlcoolismoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAlcoolismoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label12" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Já ficou afastado ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAfastamentoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAfastamentoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label13" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Pratica esporte ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbEsporteS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbEsporteN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label14" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Doenças do coração ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDoencaCoracaoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDoencaCoracaoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label15" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Faz uso de medicamento contínuo ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbMedicacoesS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbMedicacoesN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label16" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Doenças urinárias ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbUrinariaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbUrinariaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label17" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Alguma doença não mencionada ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDoencaCronicaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDoencaCronicaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label18" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Diabetes ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbDiabetesS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbDiabetesN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style11">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label19" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Já se acidentou no trabalho ?</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAcidentouS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAcidentouN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>

                                                </table>
                                          
                                                <br>
                                              
                                                <table ID="Table7" border="0" cellpadding="0" cellspacing="0" 
                                                    class="defaultFont"  style="border-collapse: collapse">   

                                                    <tr align="center">
                                                    <td>
                                                       <asp:Label ID="Label30" runat="server" SkinID="BoldFont" Font-Size="X-Small">ANTECEDENTES FAMILIARES</asp:Label>

                                                        <br />

                                                        <br />

                                                    </td>                                                    
                                                    </tr>

                                                    </table>

                                                <table ID="Table10" border="0" cellpadding="0" cellspacing="0" 
                                                    class="defaultFont"  style="border-collapse: collapse">   


                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label20" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Hipertensão Arterial</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFHipertensaoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFHipertensaoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small" />
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="lblObesidade" runat="server" SkinID="BoldFont" 
                                                                Font-Size="X-Small">-Obesidade</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFObesidadeS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFObesidadeN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label23" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Diabetes</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFDiabetesS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFDiabetesN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small" />
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label24" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Cancer</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFCancerS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFCancerN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>                                                            
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label25" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Doenças do coração</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFCoracaoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFCoracaoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small" />
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label26" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Colesterol alto</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFColesterolS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFColesterolN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>                                                            
                                                        </td>
                                                    </tr>

                                                    <tr style="border-bottom: 1px solid gray">                                                    
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label28" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Derrames cerebrais</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFDerramesS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFDerramesN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small" />
                                                        </td>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style17" align="right">
                                                            <asp:Label ID="Label29" runat="server" SkinID="BoldFont" Font-Size="X-Small">-Trat.Psiquiátricos</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAFPsiquiatricosS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAFPsiquiatricosN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>                                                            
                                                        </td>
                                                    </tr>


                                                  </table>
                                          
                                                <br>
                                                
                                                </br>
                                              
                                          
                                                </br>
                                            </td>
                                        </tr>
                                    </table>
                                </eo:PageView>
                                <eo:PageView ID="Pageview2" runat="server" Width="457px">
                                    <table ID="Table3" align="center" border="0" cellpadding="0" cellspacing="0" 
                                        class="defaultFont" width="550">
                                        <tr>
                                            <td>
                                                <table ID="Table4" border="0" cellpadding="0" cellspacing="0" 
                                                    class="defaultFont" width="545">
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td width="75">
                                                            <asp:Label ID="Label7" runat="server" SkinID="BoldFont">Altura:</asp:Label>
                                                        </td>
                                                        <td width="110">
                                                            <%--<igtxt:WebNumericEdit id="wneAltura" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Float" AutoPostBack="true" OnValueChange="wneAltura_ValueChange"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wneAltura" runat="server" width="90px" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td width="65">
                                                            <asp:Label ID="Label9" runat="server" SkinID="BoldFont">Peso(Kg):</asp:Label>
                                                        </td>
                                                        <td width="110">
                                                            <%--<igtxt:WebNumericEdit id="wnePeso" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Float" AutoPostBack="true" OnValueChange="wnePeso_ValueChange"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wnePeso" runat="server" width="90px" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                        <td width="50">
                                                            <asp:Label ID="DUM" runat="server" SkinID="BoldFont">D.U.M.:</asp:Label>
                                                        </td>
                                                        <td width="125">
                                                            <%--<igtxt:WebDateTimeEdit id="wdtDUM" runat="server" HorizontalAlign="Left" DisplayModeFormat="dd/MM/yyyy" ImageDirectory=" " Width="90px"></igtxt:WebDateTimeEdit></TD>--%>
                                                            <asp:TextBox ID="wdtDUM" runat="server" width="90px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td width="75">
                                                            <asp:Label ID="Label40" runat="server" SkinID="BoldFont">PA(mmHg):</asp:Label>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtPA" runat="server" Width="90px"></asp:TextBox>
                                                        </td>
                                                        <td width="65">
                                                            <asp:Label ID="Label21" runat="server" SkinID="BoldFont">Pulso:</asp:Label>
                                                        </td>
                                                        <td width="110">
                                                            <%--<igtxt:WebNumericEdit id="wnePulso" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Int"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wnePulso" runat="server" width="90px"></asp:TextBox>
                                                        </td>
                                                        <td width="50">
                                                            <asp:Label ID="Label3325" runat="server" SkinID="BoldFont">IMC:</asp:Label>
                                                        </td>
                                                        <td width="125">
                                                            <asp:Label ID="lblIMC" runat="server" BackColor="lightyellow"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br>
                                                <table ID="Table6" border="0" cellpadding="0" cellspacing="0" 
                                                    class="defaultFont" width="500">
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style10">
                                                        </td>
                                                        <td align="center" class="style7">
                                                            <asp:Label ID="Label38" runat="server" SkinID="BoldFont">Alteração</asp:Label>
                                                        </td>
                                                        <td align="center" class="style8">
                                                        </td>
                                                        <td class="style9">
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:Label ID="Label39" runat="server" SkinID="BoldFont">Alteração</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style10">
                                                            <asp:Label ID="Label11" runat="server" SkinID="BoldFont">-Pele e anexos</asp:Label>
                                                        </td>
                                                        <td align="center" class="style7">
                                                            <asp:CheckBox ID="ckbPeleS" runat="server" CssClass="defaultFont" Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbPeleN" runat="server" CssClass="defaultFont" Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style8">
                                                        </td>
                                                        <td class="style9">
                                                            <asp:Label ID="Label34" runat="server" SkinID="BoldFont">-Pulmões</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbPulmoesS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbPulmoesN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style10">
                                                            <asp:Label ID="Label31" runat="server" SkinID="BoldFont">-Osteo muscular</asp:Label>
                                                        </td>
                                                        <td align="center" class="style7">
                                                            <asp:CheckBox ID="ckbOsteoS" runat="server" CssClass="defaultFont" Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbOsteoN" runat="server" CssClass="defaultFont" Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style8">
                                                        </td>
                                                        <td class="style9">
                                                            <asp:Label ID="Label35" runat="server" SkinID="BoldFont">-Abdomem</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbAbdomemS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbAbdomemN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style10">
                                                            <asp:Label ID="Label32" runat="server" SkinID="BoldFont">-Cabeça e pescoço</asp:Label>
                                                        </td>
                                                        <td align="center" class="style7">
                                                            <asp:CheckBox ID="ckbCabecaS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbCabecaN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style8">
                                                        </td>
                                                        <td class="style9">
                                                            <asp:Label ID="Label36" runat="server" SkinID="BoldFont">-Membros superiores</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbMSS" runat="server" CssClass="defaultFont" Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbMSN" runat="server" CssClass="defaultFont" Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="10">
                                                        </td>
                                                        <td class="style10">
                                                            <asp:Label ID="Label33" runat="server" SkinID="BoldFont">-Coração</asp:Label>
                                                        </td>
                                                        <td align="center" class="style7">
                                                            <asp:CheckBox ID="ckbCoracaoS" runat="server" CssClass="defaultFont" 
                                                                Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbCoracaoN" runat="server" CssClass="defaultFont" 
                                                                Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                        <td align="center" class="style8">
                                                        </td>
                                                        <td class="style9">
                                                            <asp:Label ID="Label37" runat="server" SkinID="BoldFont">-Membros inferiores</asp:Label>
                                                        </td>
                                                        <td align="center" width="90">
                                                            <asp:CheckBox ID="ckbMIS" runat="server" CssClass="defaultFont" Text="Sim"  Font-Size="X-Small"/>
                                                            <asp:CheckBox ID="ckbMIN" runat="server" CssClass="defaultFont" Text="Não"  Font-Size="X-Small"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                </eo:PageView>
                                <eo:PageView ID="Pageview3" runat="server">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" 
                                        class="defaultFont" width="650">
                                        <tr>
                                            <td align="center">
                                                <table align="center" border="0" cellpadding="0" cellspacing="0" 
                                                    width="650">
                                                    <tr>
                                                        <td align="left" valign="top" class="style16">
                                                            <asp:Label ID="Label1" runat="server" SkinID="BoldFont" Text="Fatores de Risco"></asp:Label>
                                                            <asp:ListBox ID="lsbFatorRisco" runat="server" Rows="9" Width="221px" 
                                                                CausesValidation="True"></asp:ListBox>
                                                            &nbsp;<br />
                                                            
                                                                        <asp:Label ID="lblTotFatRisco" runat="server"></asp:Label>
                                                                  
                                                            <asp:Button ID="btnNovoFatorRisco" runat="server" Text="Novo Fator de Risco" 
                                                                Width="135px" onclick="btnNovoFatorRisco_Click" />
                                                        </td>
                                                        <td align="center" valign="top" class="style15">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="img/5pixel.gif" />
                                                            <br />
                                                            <br />
                                                            <asp:ImageButton ID="imbAddAll" runat="server" Enabled="False" 
                                                                ImageUrl="img/rightAllDisabled.jpg" OnClick="imbAddAll_Click" 
                                                                ToolTip="Adicionar todos" Width="16px" />
                                                            <br />
                                                            <br />
                                                            <asp:ImageButton ID="imbAdd" runat="server" Enabled="False" 
                                                                ImageUrl="img/rightDisabled.jpg" OnClick="imbAdd_Click" 
                                                                ToolTip="Adicionar selecionados" />
                                                            <br />
                                                            <br />
                                                            <asp:ImageButton ID="imbRemove" runat="server" Enabled="False" 
                                                                ImageUrl="img/leftDisabled.jpg" OnClick="imbRemove_Click" 
                                                                ToolTip="Remover selecionados" />
                                                            <br />
                                                            <br />
                                                            <asp:ImageButton ID="imbRemoveAll" runat="server" Enabled="False" 
                                                                ImageUrl="img/leftAllDisabled.jpg" OnClick="imbRemoveAll_Click" 
                                                                ToolTip="Remover todos" />
                                                        </td>
                                                        <td align="left" valign="top" width="260">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="Label2" runat="server" SkinID="BoldFont" 
                                                                Text="Fatores de Risco selecionados"></asp:Label>
                                                            <br />
                                                            &nbsp;&nbsp;
                                                            <asp:ListBox ID="lsbFatorRiscoSel" runat="server" Rows="9" 
                                                                SelectionMode="Multiple" SkinID="YellowList" Width="203px"></asp:ListBox>
                                                            <asp:Label ID="lbl_Atualiza" runat="server" Text="0" Visible="False"></asp:Label>
                                                            <br />
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" 
                                                                class="defaultFont" width="240">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="lblTotFatRiscoSel" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </eo:PageView>
                                <eo:PageView ID="Pageview4" runat="server">
                                    <table ID="Table11" align="center" border="0" cellpadding="0" cellspacing="0" 
                                        class="defaultFont" width="550">
                                        <tr>
                                            <td align="center" class="b">
                                                <strong>Anotações - Impresso no PCI<br /> </strong>
                                                <asp:TextBox ID="txtAnotacoes" runat="server" Height="102px" Rows="11" 
                                                    tabIndex="1" TextMode="MultiLine" Width="530px" MaxLength="1000" 
                                                    Font-Size="Small"></asp:TextBox>
                                                <br>
                                                <asp:Button ID="btnResetText" runat="server" OnClick="btnResetText_Click" 
                                                    Text="Atualizar anotações" Width="140px" Font-Size="X-Small" />
                                                <br />
                                                <br>
                                                <br></br>
                                                <br></br>
                                                </br>
                                                </br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="b">
                                                <strong>Observações - Apenas visualização</strong><br />
                                                <asp:TextBox ID="txtObs" runat="server" Height="95px" Rows="11" tabIndex="1" 
                                                    TextMode="MultiLine" Width="530px" MaxLength="255" Font-Size="Small"></asp:TextBox>
                                                <br>
                                                </td>
                                                </tr>

                                                <tr>
                                                <td align="left" class="b">
                                                <br>
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Altura" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para trabalho em altura" />
                                                <br />
                                                    &nbsp;&nbsp;&nbsp;
                                                <asp:CheckBox ID="chk_apt_Confinado" runat="server" Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para trabalho em espaços confinados" />
                                                <br />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_apt_Transportes" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para operar equipamentos de transporte motorizados" />
                                                <br>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_apt_Submerso" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para atividades submersas" />
                                                <br>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_apt_Eletricidade" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para serviço em eletricidade" />
                                                <br>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_apt_Aquaviarios" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para serviços aquaviários" />
                                                <br>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chk_Apt_Alimento" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" 
                                                    Text="Indicar no ASO aptidao para manipular alimentos" />
                                                <br>
                                                   

                                                   
                                               
                                                    <br></br>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox ID="chk_Apt_Respiradores" runat="server" AutoPostBack="True" Font-Size="X-Small" Text="Indicar no ASO aptidao para uso de respiradores" />
                                                    <br>
                                                    <br></br>
                                                    </br>
                                                   

                                                   
                                               
                                                    </br>
                                                    </td>
                                        </tr>
                                    </table>
                                </eo:PageView>
                                <eo:PageView ID="Pageview5" runat="server">


                                <eo:Grid ID="grd_Complementares" runat="server" BorderColor="Black" BorderWidth="1px" 
                                    ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                    ColumnHeaderDividerOffset="6" ColumnHeaderHeight="18" FixedColumnCount="1" 
                                    Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                    Font-Underline="False" GridLineColor="240, 240, 240" GridLines="Both" 
                                    Height="260px" ItemHeight="22" KeyField="Id" 
                                    Width="701px">
                                    <itemstyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: white" />
                                            <AlternatingItemStyle CssText="background-color:#eeeeee;" />
                                            <SelectedStyle 
                                                CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                            <CellStyle 
                                                CssText="padding-left:8px;padding-top:2px; color:#black;white-space:nowrap;" />
                                        </eo:GridItemStyleSet>
                                    </itemstyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle 
                                        CssText="background-image:url('00050401');padding-left:8px;padding-top:2px;" />
                                    <Columns>
                                        <eo:StaticColumn AllowSort="True" DataField="Data" HeaderText="Data do Exame" 
                                            Name="DataExame" ReadOnly="True" SortOrder="Ascending" Text="" Width="140">
                                            <CellStyle 
                                                CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="Tipo" HeaderText="Tipo" 
                                            Name="Tipo" ReadOnly="True" Width="290">
                                            <CellStyle CssText="text-align:center" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Resultado" 
                                            Name="Tipo" ReadOnly="True" Width="220">
                                            <CellStyle CssText="text-align:center" />
                                        </eo:StaticColumn>                                       
                                    </Columns>
                                    <columntemplates>
                                        <eo:TextBoxColumn>
                                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                        </eo:TextBoxColumn>
                                        <eo:DateTimeColumn>
                                            <datepicker controlskinid="None" daycellheight="16" daycellwidth="19" 
                                                dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" 
                                                selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" 
                                                titlerightarrowimageurl="DefaultSubMenuIcon">
                                                <PickerStyle 
                                                    CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                                                <CalendarStyle 
                                                    CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                                                <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                                                <TitleArrowStyle CssText="cursor:hand" />
                                                <MonthStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                                                <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DayHoverStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                <TodayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                                                <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <DisabledDayStyle 
                                                    CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                            </datepicker>
                                        </eo:DateTimeColumn>
                                        <eo:MaskedEditColumn>
                                            <maskededit controlskinid="None" 
                                                textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                            </maskededit>
                                        </eo:MaskedEditColumn>
                                    </columntemplates>
                                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                </eo:Grid>
                                <br />
                                <asp:Label ID="lblTotRegistros" runat="server" Text="Label"></asp:Label>
                                
                                </eo:PageView>
                                
                          


                                <eo:PageView ID="Pageview6" runat="server">
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="lblAnotacoes0" runat="server" CssClass="boldFont">Arquivo do &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Prontuário ASO :</asp:Label>
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txt_Arq" runat="server" CssClass="inputBox" Height="27px" 
                                        ReadOnly="True" Rows="4" TextMode="MultiLine" Width="443px" 
                                        Font-Size="X-Small"></asp:TextBox>
                                    <br />
                                    <br />
                                    &nbsp;&nbsp;
                                    <asp:Label ID="Label6" runat="server" CssClass="boldFont">Inserir / Modificar &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Arquivo de Prontuário ASO : </asp:Label>
                                    &nbsp;&nbsp;&nbsp;<br />
                                    <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" 
                                        Font-Size="XX-Small" Width="517px" />
                                    <br />
                                    <br />
                                    <asp:Button ID="cmd_PDF" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Abrir PDF" Visible="False" OnClick="cmd_PDF_Click" />
                                    <asp:Button ID="cmd_Imagem" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" OnClick="cmd_Imagem_Click" Text="Visualizar Prontuário" Visible="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <br />
                                    <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="545px" Visible="False" Width="428px" />
                                    <br />
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<br />
                                    <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
                                </eo:PageView>




                                <eo:PageView ID="Pageview7" runat="server">

                                    
                                    <eo:Grid ID="gridAnamnese" runat="server" BorderColor="Black" 
                                            BorderWidth="1px" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" ColumnHeaderDividerImage="00050402" 
                                            FixedColumnCount="1" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                            Font-Underline="False" GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="445px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="18" ItemHeight="18" KeyField="IdClinicoTestesEspeciais"  
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
                                                DataField="IdClinicoTestesEspeciais" Name="IdClinicoTestesEspeciais" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>


                                            <eo:StaticColumn HeaderText="Tipo" AllowSort="True" 
                                                DataField="Tipo" Name="Tipo" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="140">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Exame" AllowSort="True" 
                                                DataField="Exame" Name="Exame" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="420">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:left" />
                                            </eo:StaticColumn>


                                            

                                            <eo:CustomColumn HeaderText="Resultado" 
                                                DataField="Resultado2" Name="Resultado2" ReadOnly="False" 
                                                Width="125" DataType="String"  ClientSideEndEdit="on_end_edit" ClientSideBeginEdit="on_begin_edit"  ClientSideGetText="on_column_gettext" >
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:Center;" />           
                                               <EditorTemplate>
				                                                <select id="grade_dropdown"  >
                                                                    <option>Não/Negativo</option>
					                                                <option>Sim/Positivo</option>					                                                
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
                                            Text="Salvar Resultados dos Exames Especiais" Width="209px" CssClass="buttonBox" BackColor="Silver" ForeColor="Black" />
                                        
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lkbAnamnese" runat="server" SkinID="BoldLinkButton" Width="91px"><img border="0" src="img/print.gif"> Testes Especiais</img></asp:LinkButton>
                                        
                                </eo:PageView>

                            </eo:MultiPage>
                        </div>
                        <p>
                            <table ID="Table8" align="center" border="0" cellpadding="0" cellspacing="0" 
                                class="defaultFont" width="500">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" tabIndex="1" 
                                            Text="Gravar" Width="70px" CssClass="buttonBox" />
                                        &nbsp;&nbsp;<asp:Button ID="btnExcluir" runat="server" onclick="btnExcluir_Click" 
                                            Text="Excluir" Width="70px" CssClass="buttonBox"/>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="lkbASO" runat="server" SkinID="BoldLinkButton" Width="61px"><img 
                                            border="0" src="img/print.gif"> ASO</img></asp:LinkButton>
                                        <asp:LinkButton ID="lkbPCI" runat="server" SkinID="BoldLinkButton" Width="50px"><img 
                                            border="0" src="img/print.gif"> PCI</img></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="lblAusente" runat="server">Se caso o empregado não compareceu e o 
                            Exame não foi realizado,</asp:Label>
                            <asp:LinkButton ID="lbtAusente" runat="server" onclick="lbtAusente_Click" 
                                SkinID="BoldLinkButton">clique aqui</asp:LinkButton>
                            <br />
                            <input id="txtAuxAviso" type="hidden" runat="server"/>
                            <input id="txtCloseWindow" type="hidden" runat="server"/>
                            <input id="txtExecutePost" type="hidden" runat="server"/>
                           <%-- </igmisc:WebAsyncRefreshPanel>--%>
                            <asp:Button ID="cmd_Voltar" runat="server" BackColor="#999999" 
                                CssClass="buttonFoto" Font-Size="XX-Small" onclick="cmd_Voltar_Click" 
                                Text="Voltar" Width="132px" />
                        </p>

                            </TD>
				</TR>
				<TR>
					<TD align="center">&nbsp;</TD>
				</TR>
			</TABLE>
            <input id="txtAuxiliar" type="hidden" runat="server"/>

        
	</P>
    </eo:CallbackPanel>
    
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="250px" OnButtonClick="MsgBox1_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="350px" Width="650px" OnButtonClick="MsgBox1_ButtonClick" >
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
</asp:Content>



