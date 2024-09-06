<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckMapas.aspx.cs" Inherits="Ilitera.Net.CheckMapas" %>

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
                    
                    <br />

             <div class="row">
            <div class="col-11 subtituloBG pt-2">
                <asp:Label ID="lblPPRA" runat="server" SkinID="TitleFont" CssClass="subtitulo">Mapas de Risco</asp:Label>
            </div>
            </div>
                            
                    <br />
                    <br />

                    <div class="col-8 mt-4 gx-4">
                         <asp:DropDownList ID="ddlLaudos2" runat="server" CssClass="texto form-select form-select-sm" Height="32px">                            
                         </asp:DropDownList>
                    </div>

                    <div class="col-8 mt-4 gx-4">
                        <asp:Button ID="btnProjeto" OnClick="btnProjeto_Click"  runat="server"
                      Width="233px" Text="Abrir - Mapa Risco" CssClass="btn"/>
                    </div>

                    <table id="t1" >

                    <tr>
                    <td style="HEIGHT: 15px" align="left" width="295">

            

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