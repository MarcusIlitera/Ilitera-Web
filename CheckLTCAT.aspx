<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckLTCAT.aspx.cs" Inherits="Ilitera.Net.Documentos.CheckLTCAT" Title="Ilitera.Net"  %>


<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .buttonFoto {
	font: bold 8px Arial, Helvetica, sans-serif, Tahoma;
	color:#000000;
	border-top: 1px solid #CCCCCC;
	border-right: 1px solid #666666;
	border-bottom: 1px solid #666666;
	border-left: 1px solid #CCCCCC;
	}
    </style>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid d-flex ms-5 ps-4">
      <div class="row gx-3 gy-2 w-100">
    
   <LINK href="scripts/style.css" type="text/css" rel="stylesheet">    
                   
    
          <div class="row">
            <div class="col-11 subtituloBG pt-2">
                <asp:Label ID="lblTitulo" runat="server" SkinID="TitleFont" CssClass="subtitulo">LTCAT</asp:Label>
            </div>
          </div>
                  
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lbl_Msg" runat="server"></asp:Label>
                    
                    &nbsp;&nbsp;&nbsp;

          <div class="col-8 mt-4 gx-2">
              <asp:Button ID="btnImprimirLTCAT" runat="server" CssClass="btn"  Text="Imprimir LTCAT"  />
          </div>
         
                    <br />
                    <br />
                    <br />
                  &nbsp;&nbsp;

          <div class="col-8 mt-4 gx-4">
                  <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" BackColor="#999999" CssClass="btn2" Text="Voltar" Width="132px" />
          </div>

              

 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

 </div>
  </div>

</asp:Content>

