<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Ajuda.aspx.cs" Inherits="Ilitera.Net.Ajuda" Title="Ilitera.Net" %>

<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<ig:WebSplitter runat="server" ID="Painel" Height="700px">
    <Panes>
        <ig:SplitterPane Size="20%" style="padding: 5px." BackColor="#edffeb">
            <Template>
                <uc1:Menu runat="server" ID="Menu" />
            </Template>
        </ig:SplitterPane>
        <ig:SplitterPane Size="80%" style="padding: 5px;" BackColor="#edffeb">
            <Template>
                <b>Documentos Úteis</b>
                <br />
                <asp:LinkButton ID="lblINonline" runat="server" OnClick="lblINonline_Click">
                    Instrução Normativa INSS/PRES nº 20, de 11 de outubro de 2007 - DOU de 11/10/2007
                </asp:LinkButton>
                <br />
                <asp:LinkButton ID="lblINPDF" runat="server" OnClick="lblINPDF_Click">
                    Instrução Normativa MPS/SRP nº 3, de 14 de julho de 2005 - DOU de 15/07/2005
                </asp:LinkButton>
                <br />
                <asp:LinkButton ID="lblPPP" runat="server" OnClick="lblPP_Click">
                    Perfil Profissiográfico Previdenciário (Original)
                </asp:LinkButton>
                <br />
                <asp:LinkButton ID="lblPreenchimentoPPP" runat="server" OnClick="lblPreenchimentoPPP_Click">
                    Instruções de Preenchimento do Perfil Profissiográfico Previdenciário (HTML)
                </asp:LinkButton>
                <br />
            </Template>
        </ig:SplitterPane>
    </Panes>
</ig:WebSplitter>

</asp:Content>
