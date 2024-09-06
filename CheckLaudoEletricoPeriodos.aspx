<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckLaudoEletricoPeriodos.aspx.cs" Inherits="Ilitera.Net.CheckLaudoEletricoPeriodos" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
           .style2
           {
               height: 26px;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

            <LINK href="scripts/style.css" type="text/css" rel="stylesheet">

 <div class="row">
    <div class="col-11 subtituloBG pt-2">
       <asp:Label ID="lblPPRA" runat="server" SkinID="TitleFont" CssClass="subtitulo">Laudo Elétrico</asp:Label>
   </div>
 </div>


 <div class="col-md-4 gx-4 gy-4">
    <asp:dropdownlist id="ddlLaudos2" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True">
	</asp:dropdownlist>
</div>
                  
 <div class="col-11 gx-4 gy-4">
                    <asp:Button ID="btnIntroducao"  OnClick="lkbIntroducao_Click" runat="server" Width="240px" CssClass="btn" Text="Gerar Laudo Elétrico" />
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
