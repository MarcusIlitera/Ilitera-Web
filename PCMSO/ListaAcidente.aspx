

<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListaAcidente.aspx.cs"
    Inherits="Ilitera.Net.ListaAcidente" Title="Ilitera.Net" %>

<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ig:WebSplitter runat="server" ID="TestePainel" Height="700px">
        <Panes>
            <ig:SplitterPane Size="20%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane Size="80%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>                
                        
                    <br />
                    <asp:HyperLink ID="hlkNovo" runat="server" SkinID="BoldLink" 
                        style="font-weight: 700; font-size: x-small">Novo Acidente</asp:HyperLink>
                    <ig:WebDataGrid runat="server" ID="gridAcidente" AutoGenerateColumns="false" DataKeyFields="IdAcidente"
                        Width="800" Height="440"  >
                        <Columns>
                            <ig:BoundDataField DataFieldName="DataAcidente" Key="DataAcidente" Header-Text="Data do Acidente">
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="MembroAtingido" Key="MembroAtingido" Header-Text="Membro Atingido">
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AgenteCausador" Key="AgenteCausador" Header-Text="Agente Causador">
                            </ig:BoundDataField>
                        </Columns>
                        <EditorProviders>
                            <ig:TextBoxProvider ID="TextBoxProvider">
                            </ig:TextBoxProvider>
                        </EditorProviders>
                        <Behaviors>
                            <ig:Paging PagerAppearance="Both" PageSize="20" Enabled="true" PagerMode="NextPreviousFirstLast">
                            </ig:Paging>
                            <ig:Selection Enabled="true" CellSelectType="Single">
                                <SelectionClientEvents  />
                            </ig:Selection>
                            <ig:RowSelectors Enabled="true" RowNumbering="true">
                            </ig:RowSelectors>
                        </Behaviors>
                    </ig:WebDataGrid>
                </Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>
</asp:Content>
