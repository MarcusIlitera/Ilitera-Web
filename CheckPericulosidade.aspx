<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckPericulosidade.aspx.cs" Inherits="Ilitera.Net.CheckPericulosidade" %>


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
                    
        <div class="row">
            <div class="col-11 subtituloBG pt-2">
                <asp:Label ID="lblPPRA" runat="server" SkinID="TitleFont" CssClass="subtitulo">Laudo de Periculosidade</asp:Label>
            </div>
          </div>
                    
  
                    <br />
                    <br />
          <div class="col-md-4 gx-4 gy-4">
				<asp:dropdownlist id="ddlLaudos2" runat="server" CssClass="texto form-select form-select-sm" AutoPostBack="True">
				</asp:dropdownlist>
			</div>

                    <table id="t1" >

                    <tr>
                    <td style="HEIGHT: 15px" align="left" width="295">

                        
                        <br />

                        <br />
            <div class="col-md-4 gx-4 gy-4">
                    <asp:Button ID="btnProjeto" OnClick="btnProjeto_Click"  runat="server" Width="233px" Text="Abrir - Laudo de Periculosidade" CssClass="btn"/>
                </div>

                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                        </td>

                        <td style="HEIGHT: 15px" align="left" width="295">
                        </td>
                        </tr>
                      

                        </table>

                    <br />
                    <br />
                    <br />
                 
</div>
  </div>
</asp:Content>
