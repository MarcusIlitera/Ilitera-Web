<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_AON.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_AON" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
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
        .auto-style1 {
            width: 767px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
    <div class="row gx-3 gy-2">

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

    <div class="col-12 subtituloBG pt-2">
        <asp:Label ID="lblNome" runat="server" CssClass="subtitulo" SkinID="TitleFont">Relatório de AON</asp:Label>
    </div>
	<table class="auto-style1" id="Table1" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                <eo:Grid ID="gridAON" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" FixedColumnCount="1" 
                    GridLines="Both" Height="380px" Width="1024px" ColumnHeaderDividerOffset="6" ItemHeight="30" ColumnHeaderHeight="30" KeyField="IdEmpregado" PageSize="40" FullRowMode="False" CssClass="grid" >
                    <ItemStyles>
                        <eo:GridItemStyleSet>
                            <ItemStyle CssText="background-color: #FAFAFA" />
                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                        </eo:GridItemStyleSet>
                    </ItemStyles>
                    <ColumnHeaderTextStyle CssText="" />
                    <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                    <Columns>
                        <eo:StaticColumn HeaderText="" AllowSort="True" DataField="IdEmpregado" Name="IdEmpregado" ReadOnly="True" SortOrder="Ascending" Text="" Width="0">
                            <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Colaborador" DataField="Colaborador" Name="Colaborador" ReadOnly="True" SortOrder="Ascending" Text="" Width="150">
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                        </eo:StaticColumn>

                        <eo:StaticColumn HeaderText="Evento" DataField="Evento" Name="Evento" ReadOnly="True" SortOrder="Ascending" Text="" Width="130">
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Especialidade" DataField="Especialidade" Name="Especialidade" Width="130">
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Atendimento" DataField="DataAtendimento" Name="DataAtendimento" Width="80">
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:center; vertical-align: middle;" />
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Nascimento" DataField="Nascimento" Name="Nascimento" Width="80" >
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:center; vertical-align: middle;" />                                                
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn DataField="Sexo" HeaderText="Sexo" Name="Sexo" Width="90">
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:center; vertical-align: middle;" />                                                
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Admissão" DataField="Admissao" Name="Admissao" Width="80" >
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:center; vertical-align: middle;" />                                                
                        </eo:StaticColumn>
                        
                        <eo:StaticColumn HeaderText="Descrição do Atendimento" DataField="Descricao" Name="Descricao" Width="265" >
                            <CellStyle CssText="border-bottom-color:gray;border-bottom-style:solid;border-bottom-width:1px;border-left-color:gray;border-left-style:solid;border-left-width:1px;border-right-color:gray;border-right-style:solid;border-right-width:1px;border-top-color:gray;border-top-style:solid;border-top-width:1px;font-family:Tahoma;font-size:8pt;text-align:left; vertical-align: middle;" />                                                
                        </eo:StaticColumn>

                    </Columns>
                    
                    <ColumnTemplates>
                        <eo:TextBoxColumn>
                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                        </eo:TextBoxColumn>
                        
                        <eo:DateTimeColumn>
                            <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19" DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True"
                                SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" TitleRightArrowImageUrl="DefaultSubMenuIcon">
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
                            <MaskedEdit ControlSkinID="None" TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                            </MaskedEdit>
                        </eo:MaskedEditColumn>

                    </ColumnTemplates>
                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />

                </eo:Grid>
            </td>
        </tr>
	</table>
        
        <caption>
            <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
            
        </caption>
        
        <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn" Width="100" Text="Voltar" onclick="cmd_Voltar_Click"  />

        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
                            
            
    </div>
    </div>
</asp:Content>