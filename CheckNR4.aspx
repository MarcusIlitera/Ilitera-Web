<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckNR4.aspx.cs" Inherits="Ilitera.Net.CheckNR4" %>

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
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
      <div class="row gx-3 gy-2 w-100">

        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
                    
     <%-- <ig:WebSplitter runat="server" ID="TestePainel" Height="700px">
        <Panes>
            <ig:SplitterPane Size="20%" style="padding:5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane Size="80%" style="padding: 5px;" BackColor="#edffeb">
                <Template>--%>

          <div class="row">
            <div class="col-11 subtituloBG pt-2">
                <asp:Label ID="lblTitulo" runat="server" SkinID="TitleFont" CssClass="subtitulo">Quadros NR-4</asp:Label>
            </div>
          </div>
                
                    <br />
                    <br />        
                    <br />
          <div class="col-8 mt-4 gx-4">
            <asp:Label ID="lblTitulo0" runat="server" CssClass="tituloLabel form-label col-form-label">Ano</asp:Label>
            <asp:TextBox ID="txt_Ano" runat="server" CssClass="texto form-control form-control-sm" MaxLength="4" Width="56px"></asp:TextBox>
          </div>
                    <br />
                    <br />
                    <br /> 

          <div class="col-8 mt-4 gx-4">
             <asp:Button ID="btnCronograma"  runat="server" CssClass="btn" OnClick="lkbCronograma_Click"
             ToopTip="Laudo Ergonômico - Cronograma" Text="Gerar Relatório" />
          </div>
                    <br />
                    <br />
                    <br />
                        
            <%--    </Template>
            </ig:SplitterPane>
        </Panes>
      </ig:WebSplitter>--%>

</div>
  </div>

</asp:Content>