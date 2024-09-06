<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckVasos.aspx.cs" Inherits="Ilitera.Net.CheckVasos" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

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
        .auto-style1 {
            height: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
     <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">


        <%-- subtitulo --%>
        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="lblPPRA" runat="server" SkinID="TitleFont" CssClass="subtitulo">Vasos de Pressão</asp:Label>
        </div>

          <div class="col-11 mb-3">
            <div class="row">

             <div class="col-4 gx-2 gy-2">
                 <asp:dropdownlist ID="ddlLaudos2" OnSelectedIndexChanged="ddlLaudos2_SelectedIndexChanged" runat="server" CssClass="texto form-select form-select-sm" 
                         AutoPostBack="True"> </asp:dropdownlist>
             </div>

          <div class="col-11 gx-2 gy-4">
             <asp:Button ID="btnProjeto" OnClick="btnProjeto_Click"  runat="server" Width="233px" Text="Visualizar Projeto" CssClass="btn"/>
            </div>

           <div class="col-11 gx-2 gy-4">
                <asp:Label ID="lblInspecao" runat="server" CssClass="tituloLabel form-label" Text="Inspeções:" Visible="False"></asp:Label>
                <asp:dropdownlist ID="ddlInspecao" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True" Visible="False"> </asp:dropdownlist>
               </div>

         </div>
          </div>

            <div class="col-12">
              <div class="row">
                 <div class="col-12 text-center" style="padding-left: 0px !important;">
                  <asp:Button ID="btnInspecao" OnClick="btnInspecao_Click"  runat="server" Width="207px" Text="Visualizar Inspeção - Laudo Antigo" CssClass="btn" Visible="False"/>
                  <asp:Button ID="btnInspecaoNovo" OnClick="btnInspecaoNovo_Click"  runat="server" Width="207px" Text="Visualizar Inspeção - Laudo Novo" CssClass="btn" Visible="False"/>
           </div>
              </div>
            </div>

                 
   </div>
     </div>
</asp:Content>
