<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="visualizarFuncao_CBO.aspx.cs"   Inherits="Ilitera.Net.visualizarFuncao_CBO" Title="Ilitera.Net" %>

<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 33px;
        }
        .style2
        {
            width: 36px;
        }
        .style3
        {
            width: 37px;
        }
        .style6
        {
            font-size: x-small;
            font-weight: bold;
        }
        .style7
        {
            width: 757px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <ig:WebSplitter runat="server" ID="Painel" Height="1250px">
        <Panes>
            <ig:SplitterPane Size="20%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane Size="80%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    

                    <span style="font-size:12px;">Informações do Empregado</span><b style="color:Red;font-size:15px;padding-left:5px;"><asp:Label ID="lblNomeEmpregado" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b>
                    <br /><br />

                    <br />

                    <table>
                        <caption>
                        </caption>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmd_Acidente" runat="server" Height="24px" 
                                    onclick="cmd_Acidente_Click" Text="Acidentes" Width="102px" />
                            </td>
                        </tr>
                    </table>
                    <ig:WebTab ID="wtClassificacaoFuncional" runat="server" Height="2000px" 
                        Width="800px">
                        <Tabs>
                            <ig:ContentTabItem runat="server" Text="Detalhes da Classificação Funcional">
                                <Template>


                             <ig:WebDataGrid ID="gridEmpregados" runat="server" AutoGenerateColumns="False"  EnableDataViewState="true"
                            DataKeyFields="IdClassificacaoFuncional" Height="240px" 
                             OnCellSelectionChanged="gridEmpregados_CellSelectionChanged" Width="760px"  
                            BorderColor="#6699FF" BorderStyle="Solid" BorderWidth="2px" CellSpacing="1">
                            <Columns>

                                <ig:BoundDataField DataFieldName="Funcao" 
                                    Header-Text="Função" Key="Funcao" Width="210px">
                                    <Header Text="Função" />
                                </ig:BoundDataField>

                                <ig:BoundDataField DataFieldName="Setor" 
                                    Header-Text="Setor" Key="Setor" Width="210px">
                                    <Header Text="Setor" />
                                </ig:BoundDataField>


                                <ig:BoundDataField DataFieldName="InicioFuncao" 
                                    Header-Text="Data Início" Key="InicioFuncao" Width="120px">
                                    <Header Text="Data Início" />
                                </ig:BoundDataField>



                                <ig:BoundDataField DataFieldName="TerminoFuncao" 
                                    Header-Text="Data Término" Key="TerminoFuncao" Width="120px">
                                    <Header Text="Data Término" />
                                </ig:BoundDataField>

                            </Columns>

                           <editorproviders>
                                <ig:TextBoxProvider ID="TextBoxProvider">
                                </ig:TextBoxProvider>
                            </editorproviders>
                            <behaviors>
                                <ig:Paging Enabled="true" PagerAppearance="Both" 
                                    PagerMode="NextPreviousFirstLast" PageSize="10">
                                </ig:Paging>
                                <ig:Selection CellSelectType="Single" Enabled="true">
                                    <SelectionClientEvents CellSelectionChanged="gridEmpregados_CellSelectionChanged" />
                                </ig:Selection>
                                <ig:RowSelectors Enabled="true" RowNumbering="true">
                                </ig:RowSelectors>
                            </behaviors>
                            </ig:WebDataGrid>
                                    




                                    <table>
                                    <tr>
                                    <td align="center" class="style7">
                                     <asp:Button ID="cmd_Novo"     runat="server" Height="19px"  
                                            onclick="cmd_Novo_Click" Text="Nova Classificação" 
                                            Width="140px"                                                 BackColor="#FF8080" 
                                      Font-Size="X-Small" />
                                      </td>
                                      </tr>
                                      </table>

                                      <table>
                                                                                 
                                        <tr>
                                            <td>
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                           
                                        <tr>
                                            <td align="right" class="style1">
                                                <br />
                                                <asp:Label ID="lblInicioFuncao" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInicioFuncao" runat="server" Width="100px" MaxLength="10" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblTerminoFuncao" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                                <asp:TextBox ID="txtTerminoFuncao" runat="server" Width="100px" MaxLength="10" Font-Size="X-Small"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="lblFuncao" runat="server" Text="Função"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFuncao" runat="server" Width="200px" Font-Size="X-Small"></asp:TextBox>
                                                <br />
                                                <asp:DropDownList ID="cmb_Funcao1" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" class="style3">
                                                </td>
                                            <td  class="style3">
                                               


                                                </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style2">
                                                <asp:Label ID="lblSetor" runat="server" Text="Setor"></asp:Label>
                                            </td>
                                            <td class="style3">
                                                <asp:TextBox ID="txtSetor" runat="server" Width="200px" Font-Size="X-Small"></asp:TextBox>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor1" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px"  >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" class="style3">
                                                <asp:Label ID="lblCargo" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                                <asp:TextBox ID="txtCargo" runat="server" Width="200px" Font-Size="X-Small"></asp:TextBox>
                                                <br />
                                                &nbsp;
                                                <asp:DropDownList ID="cmb_Cargo1" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px"  >
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td>                                                
                                            <asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
                                            <asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>
                                                <asp:Button ID="cmd_Alterar1" runat="server" Height="19px" 
                                                    onclick="cmd_Alterar1_Click" Text="Salvar Dados" Width="102px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />
                                           </td>
                                           <td>
                                                <asp:Button ID="cmd_Excluir1" runat="server" Height="19px" 
                                                    onclick="cmd_Excluir1_Click" Text="Excluir Classif.Func." Width="162px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />

                                           </td>

                                           <td>
                                           
                                               <asp:Button ID="cmdGHE_1" runat="server" BackColor="#FF3300" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmdGHE_1_Click" Text="Mudar GHE" 
                                                   Width="86px" Visible="False" />
                                           
                                           </td>

                                           <td>
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_1" runat="server" BackColor="#CCFFFF" 
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="123px" 
                                                   onselectedindexchanged="lst_1_SelectedIndexChanged">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_1" runat="server" Text="lbl_Id_1" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_1" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Add_1_Click" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_1" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Remove_1_Click" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <asp:ListBox ID="lst_Sel_1" runat="server" 
                                                   BackColor="#FFCC99" Font-Names="Courier New" Font-Size="X-Small" Height="58px" 
                                                   Visible="False" Width="364px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_1_Cop" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_1_ID" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_1_Id" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                           
                                           
                                               <asp:Label ID="lbl_Id" runat="server" Text="0" Visible="False"></asp:Label>
                                           
                                           
                                           </td>

                                        </tr>
                                    </table>

                                    
                                    
                                </Template>
                            </ig:ContentTabItem>
                            <ig:ContentTabItem runat="server" Text="Descrição da Função">
                                <Template>
                                    <br />
                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" 
                                        Text="Função  : "></asp:Label>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmb_Setor" runat="server" AutoPostBack="True" 
                                        Font-Size="X-Small" Height="16px" 
                                        onselectedindexchanged="cmb_Setor_SelectedIndexChanged" Width="425px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="cmb_SetorD" runat="server" Font-Size="X-Small" 
                                        Height="16px" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                        Visible="False" Width="378px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="cmb_CBOID" runat="server" Font-Size="X-Small" 
                                        Height="16px" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                        Visible="False" Width="378px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:DropDownList ID="cmb_SetorID" runat="server" Font-Size="X-Small" 
                                        Height="16px" onselectedindexchanged="cmb_Setor_SelectedIndexChanged" 
                                        Visible="False" Width="378px">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txt_SetorAlt" runat="server" BackColor="#FFFFCC" 
                                        Height="223px" TextMode="MultiLine" Width="515px"></asp:TextBox>
                                    <br />
                                    <br />
                                    <br />
                                    <span class="style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CBO da Função<br /> </span>&nbsp;Filtro 
                                    :&nbsp;&nbsp;<asp:TextBox ID="txt_CBO" runat="server" AutoPostBack="True" 
                                        ontextchanged="txt_CBO_TextChanged" Width="102px" BackColor="#FFFFCC"></asp:TextBox>
                                    &nbsp;&nbsp; <asp:DropDownList ID="cmb_CBO" runat="server" AutoPostBack="True" 
                                        Font-Size="X-Small" Height="20px" Width="340px" 
                                        onselectedindexchanged="cmb_CBO_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Button ID="cmd_Atualizar_DFuncao" runat="server" BackColor="#FFCC66" 
                                        Font-Bold="True" Font-Size="XX-Small" Height="24px" onclick="cmd_Atualizar_DFuncao_Click" 
                                        Text="Salvar alterações na descrição do Setor" Width="281px" />
                                    <br />
                                </Template>
                            </ig:ContentTabItem>
                        </Tabs>
                    </ig:WebTab>
                </Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>
</asp:Content>
