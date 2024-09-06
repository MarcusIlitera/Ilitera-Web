<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckLaudoSPDAPeriodos.aspx.cs" Inherits="Ilitera.Net.CheckLaudoSPDAPeriodos" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        .inputBox
        {}
        .largeboldfont
        {}
    </style>

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
        <div class="row gx-3 gy-2">

    <LINK href="scripts/style.css" type="text/css" rel="stylesheet">

            <div class="col-11">
                <div class="row">
                    
                    <div class="col-md-8 mb-3">
                        <asp:Label ID="lblPPRA" runat="server" Text="Laudo SPDA Gerado no Sistema" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:dropdownlist ID="ddlLaudos2" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                    </div>

                    <div class="col-4"></div>

                    <div class="col-md-3 mb-3">
                        <asp:Button ID="btnIntroducao" OnClick="lkbIntroducao_Click" runat="server" CssClass="btn" Text="Gerar Laudo SPDA" />
                    </div>

                    <div class="col-9"></div>

                    <div class="col-md-8 mb-3">
                        <asp:Label ID="Label1" runat="server" Text="RTI Carregado em arquivo" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:dropdownlist ID="cmb_RTI" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:dropdownlist>
                    </div>

                    <div class="col-4"></div>

                    <div class="col-md-3 mb-3">
                        <asp:Button ID="btnRTI" OnClick="btnRTI_Click" runat="server" Text="Abrir - RTI" CssClass="btn"/>
                    </div>

                    <div class="col-9"></div>

                    <div class="col-md-8 mb-3">
                        <asp:Label ID="Label2" runat="server" Text="SPDA Carregado em arquivo" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:dropdownlist ID="cmb_SPDA" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True"></asp:dropdownlist>
                    </div>

                    <div class="col-4"></div>

                    <div class="col-md-3 mb-3">
                        <asp:Button ID="btnSPDA" OnClick="btnSPDA_Click"  runat="server" Text="Abrir - SPDA" CssClass="btn"/>
                    </div>
                </div>
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