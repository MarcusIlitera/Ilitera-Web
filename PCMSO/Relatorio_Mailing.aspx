<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="Relatorio_Mailing.aspx.cs"  Inherits="Ilitera.Net.PCMSO.Relatorio_Mailing" Title="Ilitera.Net" %>


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
        .auto-style1 {
            width: 767px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
     <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">

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
         <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label runat="server" class="subtitulo">Relatórios de Mailing</asp:Label>
        </div>

	<table class="normalFont" id="Table1" cellspacing="0" cellpadding="0" width="1000"
				border="0">
				
                 <tr>
                    
                       <div class="col-12">
                            <div class="row" style="border: 1px solid silver; width: 91.5%; border-radius: 4px;">
                                <div class="col-2 me-2 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm mb-1">Data início</asp:Label>
                                    <asp:Textbox ID="txt_Data" runat="server" 
                                        CssClass="inputBox texto form-control form-control-sm" EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" 
                                        ImageDirectory=" " Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                </div>
                                <div class="col-2 me-4 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Data final</asp:Label>
                                    <asp:Textbox ID="txt_Data2" runat="server" CssClass="inputBox texto form-control form-control-sm" 
                                        EditModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " 
                                        Nullable="False" ToolTip="Data de Início">
                                    </asp:Textbox>
                                    </div>
                                <div class="col-4 me-4 gx-4 gy-2">
                                    <asp:Label runat="server" CssClass="tituloLabel form-label" Height="13">Empresa Selecionada </asp:Label>
                                    <asp:DropDownList ID="cmb_Empresa" runat="server" CssClass="texto form-select form-select-sm">
                                        <asp:ListItem Value="1">Empresa selecionada</asp:ListItem>
                                        <asp:ListItem Value="2">Empresa selecionada e projetos</asp:ListItem>
                                        <asp:ListItem Value="3">Todas empresas do grupo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                <div class="row mt-3">
                    <div class="col-10 ms-3 gx-4 gy-2">
                        <asp:CheckBox ID="chk_Attachados" runat="server" Text="Exibir apenas funcionários sem documentos attachados após 40 dias" Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="true"
                             AutoPostBack="True" />
                    </div>
                    <div class="col-10 ms-3 gx-4 gy-1">
                        <asp:CheckBox ID="chk_Sem_Resultado" runat="server" Text="Exibir apenas funcionários sem resultado após 40 dias" Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="false"
                             AutoPostBack="True" />
                    </div>
                 <div class="col-10 ms-3 gx-4 gy-1">
                        <asp:CheckBox ID="chk_Sem_Agendamento" runat="server" Text="Exibir apenas mailings sem agendamento" Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="false"
                             AutoPostBack="True" />
                    </div>
                 <div class="col-10 ms-3 gx-4 gy-1">
                        <asp:CheckBox ID="chk_Nao_Enviados" runat="server" Text="Exibir apenas mailings não enviados" Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="false"
                             AutoPostBack="True" />
                    </div>
                    </div>
                <div class="col-2 ms-3 gx-4 gy-2 mb-2">
                       <asp:RadioButton ID="chk_PDF" oncheckedchanged="chk_PDF_CheckedChanged"  runat="server"  Text="Gerar PDF"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;" Checked="true" 
                             AutoPostBack="true"
                             GroupName="g3"/>
                    </div>
                <div class="col-2 gx-2 gy-2 mb-2">
                      <asp:RadioButton ID="chk_CSV" oncheckedchanged="chk_CSV_CheckedChanged" runat="server" Text="Gerar CSV"  Cssclass="texto form-check-label border-0 bg-transparent" Style="text-align: left;"  Checked="true" 
                             AutoPostBack="true" 
                             GroupName="g3"/>
                </div>
                    <br />
                
                </td>

                </tr>


                <tr>
                <td>
                
                    <br />
                
                <b>
                    
                    &nbsp;<asp:button ID="cmd_Carregar_Grid" runat="server" onclick="cmd_Carregar_Grid_Click" 
                        Text="Carregar Grid" CssClass="btn">
                    </asp:button>
                 
                    <asp:button ID="btnemp" runat="server" onclick="btnemp_Click" 
                        Text="Gerar Relatório" CssClass="btn">
                    </asp:button>

                    </b>
              
                    <br />
                
                </td>

                </tr>

                <tr>
                <td>
                
                    &nbsp;</td>

                </tr>
                <tr>
                <td>
                
                    &nbsp;</td>

                </tr>

                <tr>
                <td>
                
                    
                  <asp:GridView ID="UltraWebGridMailing" runat="server" Height="300px"
        Width="1000px" AutoGenerateColumns="False" CellPadding="4"
        EnableModelValidation="True" ForeColor="#333333" AllowPaging="True" 
        AllowSorting="True" Font-Size="X-Small" 
        PageSize="25" Visible="False"
onselectedindexchanged="UltraWebGridMailing_SelectedIndexChanged"  OnPageIndexChanging="UltraWebGridMailing_PageIndexChanging"                      >
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                          <ControlStyle Width="220px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Colaborador" HeaderText="Colaborador">
                          <ControlStyle Width="230px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Data_Planejada" HeaderText="Data Planejada" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="100px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Numero_Mailing" HeaderText="Mailing" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="70px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="DataEnvio" HeaderText="Envio Mailing" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="80px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>   
                          <asp:BoundField DataField="Status_Envio" HeaderText="Status Envio" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="90px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="eMail_Envio" HeaderText="e-Mail" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="250px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Status_Exame" HeaderText="Status Exame" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="90px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Data_Exame" HeaderText="Data Exame" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Data_Digitalizacao" HeaderText="Digitalização" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                          <asp:BoundField DataField="Resultado_Exame" HeaderText="Resultado" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px" />

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                          </asp:BoundField>
                      </Columns>
                      <EditRowStyle BackColor="#2461BF" />
                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                      <HeaderStyle BackColor="#FF5050" Font-Bold="True" ForeColor="White" />
                      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                      <RowStyle BackColor="#EFF3FB" />
                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                  </asp:GridView>



                    
                </td>
                
                </tr>

            
        <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
            </table>

 </div>
  </div>


<%--		</form>
	</body>
</HTML>
--%>



    </asp:Content>