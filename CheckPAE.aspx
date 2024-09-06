<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckPAE.aspx.cs" Inherits="Ilitera.Net.CheckPAE" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
                    

            <div class="col-11">
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblPPRA" runat="server" Text="PAE Plano de Atendimento de Emergência" CssClass="tituloLabel form-label"></asp:Label>
                        <asp:dropdownlist ID="ddlLaudos2" runat="server" CssClass="texto form-select" AutoPostBack="True"></asp:dropdownlist>
                    </div>

                    <div class="col-4"></div>

                    <div class="col-md-3 mt-3">
                        <asp:Button ID="btnProjeto" OnClick="btnProjeto_Click"  runat="server" Text="Abrir - PAE" CssClass="btn"/>
                    </div>
                </div>
            </div>
                    
                    

                    <table id="t1" >

                    <td style="HEIGHT: 15px" align="left" width="295">

                        
                        

                        </td>

                        <td style="HEIGHT: 15px" align="left" width="295">
                        </td>
                        </tr>
                      

                        </table>

                    
        </div>      
     </div>               
</asp:Content>