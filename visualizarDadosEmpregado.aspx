<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="visualizarDadosEmpregado.aspx.cs"   Inherits="Ilitera.Net.visualizarDadosEmpregado" Title="Ilitera.Net" %>

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
        .style5
        {
            width: 64px;
        }
        .style6
        {
            font-size: x-small;
            font-weight: bold;
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
                    <asp:Button ID="cmd_Atualizar" runat="server" Height="24px" 
                        onclick="cmd_Atualizar_Click" Text="Salvar Dados / Classif.Funcional" 
                        Width="217px" />
                    <br /><br />
                    <table width="800px">
                    
                        <tr>
                            <td align="right" class="style5">
                                
                            </td>
                            <td align="left">
                                
                            </td>
                            <td align="right">
                                
                            </td>
                            <td align="left">
                                
                            </td>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Número da Foto:"></asp:Label>
                                <br />
                                <br />                                
                            </td>
                            <td  style="border-width:thin; border-color:Gray; border-style:solid"  >
                                <ig:WebTextEditor ID="txtFoto" runat="server" Width="80px" MaxLength="4">
                                </ig:WebTextEditor>
                                <br />
                                <br />
                                <asp:Button ID="cmd_Refresh" runat="server" BackColor="#FF3300" 
                                    Font-Size="XX-Small" Height="18px" onclick="cmd_Refresh_Click" Text="Refresh" 
                                    Width="70px" />
                            </td>
                            <td align="right">
                                
                            </td>
                            <td align="left">
                                <asp:Image ID="ImgFunc" runat="server" Height="102px" BorderColor="#660033" 
                                    BorderStyle="Inset" BorderWidth="2px" />
                            </td>
                        </tr>

                        <tr>
                            <td align="right" class="style5">
                                <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebTextEditor ID="txtNomeEmpregado" Enabled="false" runat="server" Width="200px">
                                </ig:WebTextEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblMatricula" runat="server" Text="RE(Matrícula)"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebTextEditor ID="txtMatricula" runat="server" Width="100px">
                                </ig:WebTextEditor>
                            </td>
                            <td>
                                <asp:Label ID="lblPISPASEP" runat="server" Text="PIS/PASEP"></asp:Label>
                            </td>
                            <td>
                                <ig:WebTextEditor ID="txtPISPASEP" runat="server" Width="100px">
                                </ig:WebTextEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTerceiroEstagiario" runat="server" Text="Terc/Estag" Style="visibility: hidden;"></asp:Label>
                            </td>
                            <td align="left">
                                <input id="cboxTerceiroEstagiario" type="checkbox" style="visibility: hidden;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style5">
                                <asp:Label ID="lblSexo" runat="server" Text="Sexo"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebDropDown ID="cbSexo" runat="server" Width="203px">
                                    <Items>
                                        <ig:DropDownItem Selected="False" Text="Masculino" Value="M">
                                        </ig:DropDownItem>
                                        <ig:DropDownItem Selected="False" Text="Feminino" Value="F">
                                        </ig:DropDownItem>
                                    </Items>
                                </ig:WebDropDown>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblIdentidade" runat="server" Text="Identidade"></asp:Label>
                            </td>
                            <td>
                                <ig:WebTextEditor ID="txtRG" runat="server" Width="100px">
                                </ig:WebTextEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCPF" runat="server" Text="CPF"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebTextEditor ID="txtCPF" runat="server" Width="100px">
                                </ig:WebTextEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNascimento" runat="server" Text="Nascimento"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="txtDataNascimento" runat="server" Width="90px">
                                </ig:WebDateTimeEditor>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style5">
                                <asp:Label ID="lblCTPS_Num" runat="server" Text="CTPS"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebTextEditor ID="txtCTPS_Num" runat="server" Width="50px">
                                </ig:WebTextEditor>
                                &nbsp
                                <asp:Label ID="lblCTPS_SERIE" runat="server" Text="Série"></asp:Label>
                                <ig:WebTextEditor ID="txtCTPS_Serie" runat="server" Width="50px">
                                </ig:WebTextEditor>
                                &nbsp
                                <asp:Label ID="lblCTPS_UF" runat="server" Text="UF"></asp:Label>
                                <ig:WebTextEditor ID="txtCTPS_UF" runat="server" Width="26px">
                                </ig:WebTextEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDataDemissao" runat="server" Text="Admissão"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="txtDataAdmissao" runat="server" Width="100px">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblDemissao" runat="server" Text="Demissão"></asp:Label>
                            </td>
                            <td>
                                <ig:WebDateTimeEditor ID="txtDataDemissao" runat="server" Width="100px">
                                </ig:WebDateTimeEditor>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblApelido" runat="server" Text="Apelido"></asp:Label>
                            </td>
                            <td>
                                <ig:WebTextEditor ID="txtApelidoEmpregado" runat="server" Width="90px">
                                </ig:WebTextEditor>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="style5">
                                <asp:Label ID="lblGHE_Atual" runat="server" Text="GHE Atual:" Visible="False"></asp:Label>
                            </td>
                            <td align="left">
                                <ig:WebTextEditor ID="txtGHE_Atual" runat="server" Width="200px" 
                                    Visible="False">
                                </ig:WebTextEditor>
                            </td>

                            <td>
                                <asp:Button ID="cmdGHE" runat="server" BackColor="#FF3300"   
                                    Font-Size="XX-Small" Height="18px"  Text="Mudar GHE"    Width="86px" 
                                    onclick="cmdGHE_Click" Visible="False" />                            
                            </td>
                            <td>
                            
                            </td>

                            </tr>

                            <tr>

                            <td align="right" class="style5">
                                <asp:Label ID="lblGHE" runat="server" Text="Novo GHE:" Visible="False"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmb_GHE" runat="server" AutoPostBack="True" 
                                    Font-Size="X-Small" Height="20px" Width="206px" Visible="False">
                                </asp:DropDownList>
                            </td>

                        </tr>
                    </table>

                    <br />

                    <table>
                        <caption>
                        </caption>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="cmd_Acidente" runat="server" Enabled="False" Height="24px" 
                                    onclick="cmd_Acidente_Click" Text="Acidentes" Width="102px" />
                            </td>
                        </tr>
                    </table>
                    <ig:WebTab ID="wtClassificacaoFuncional" runat="server" Height="2000px" 
                        Width="800px">
                        <Tabs>
                            <ig:ContentTabItem runat="server" Text="Detalhes da Classificação Funcional">
                                <Template>
                                    <table>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="lblInicioFuncao" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtInicioFuncao" runat="server" 
                                                    style="text-align: left" Width="100px" >
                                                </ig:WebDateTimeEditor>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblTerminoFuncao" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtTerminoFuncao" runat="server" 
                                                    style="text-align: left" Width="100px" 
                                                    >
                                                </ig:WebDateTimeEditor>
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
                                                <ig:WebTextEditor ID="txtFuncao" runat="server" Width="200px" 
                                                    >
                                                </ig:WebTextEditor>
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
                                                <ig:WebTextEditor ID="txtSetor" runat="server" Width="200px" >
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor1" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px"  >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" class="style3">
                                                <asp:Label ID="lblCargo" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td class="style3">
                                                <ig:WebTextEditor ID="txtCargo" runat="server" Width="200px" >
                                                </ig:WebTextEditor>
                                                <br />
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
                                                    onclick="cmd_Alterar1_Click" Text="Alterar Dados" Width="102px" 
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
                                           
                                           
                                           </td>

                                        </tr>
                                    </table>

                                    
                                    <table >
                                    <tr>
                                    <td>
                                    <hr style="height: 4px; width: 1000px" />
                                    </td>
                                    </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label1" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtInicioFuncao2" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label2" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtTerminoFuncao2" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
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
                                                <asp:Label ID="Label3" runat="server" Text="Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtFuncao2" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Funcao2" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>

                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label4" runat="server" Text="Setor"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtSetor2" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor2" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>

                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtCargo2" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Cargo2" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            </tr>

                                            <tr>
                                            <td>                                                
                                                <asp:Button ID="cmd_Alterar2" runat="server" Height="19px" 
                                                    onclick="cmd_Alterar2_Click" Text="Alterar Dados" Width="102px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />
                                           </td>
                                           <td>
                                                <asp:Button ID="cmd_Excluir2" runat="server" Height="19px" 
                                                    onclick="cmd_Excluir2_Click" Text="Excluir Classif.Func." Width="162px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />

                                           </td>


                                           <td>
                                           
                                               <asp:Button ID="cmdGHE_2" runat="server" BackColor="#FF3300" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmdGHE_2_Click" Text="Mudar GHE" 
                                                   Width="86px" Visible="False" />
                                           
                                           </td>

                                           <td>
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_2" runat="server" BackColor="#CCFFFF" 
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="123px" 
                                                   onselectedindexchanged="lst_1_SelectedIndexChanged">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_2" runat="server" Text="lbl_Id_2" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_2" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Add_2_Click" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_2" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Remove_2_Click" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <asp:ListBox ID="lst_Sel_2" runat="server" 
                                                   BackColor="#FFCC99" Font-Names="Courier New" Font-Size="X-Small" Height="58px" 
                                                   Visible="False" Width="364px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_2_Cop" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_2_ID" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_2_Id" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                           
                                           
                                           </td>

                                        </tr>

                                    </table>


                                    <table >
                                    <tr>
                                    <td>
                                    <hr style="height: 4px; width: 1000px" />
                                    </td>
                                    </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label6" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtInicioFuncao3" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtTerminoFuncao3" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
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
                                                <asp:Label ID="Label8" runat="server" Text="Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtFuncao3" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Funcao3" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label9" runat="server" Text="Setor"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtSetor3" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor3" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label10" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtCargo3" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Cargo3" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            </tr>


                                            <tr>
                                            <td>                                                
                                                <asp:Button ID="cmd_Alterar3" runat="server" Height="19px" 
                                                    onclick="cmd_Alterar3_Click" Text="Alterar Dados" Width="102px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />
                                           </td>
                                           <td>
                                                <asp:Button ID="cmd_Excluir3" runat="server" Height="19px" 
                                                    onclick="cmd_Excluir3_Click" Text="Excluir Classif.Func." Width="162px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />

                                           </td>



                                           <td>
                                           
                                               <asp:Button ID="cmdGHE_3" runat="server" BackColor="#FF3300" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmdGHE_3_Click" Text="Mudar GHE" 
                                                   Width="86px" Visible="false"  />                                           
                                           </td>

                                           <td>
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_3" runat="server" BackColor="#CCFFFF" 
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="123px" 
                                                   onselectedindexchanged="lst_1_SelectedIndexChanged">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_3" runat="server" Text="lbl_Id_3" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_3" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Add_3_Click" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_3" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Remove_3_Click" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <asp:ListBox ID="lst_Sel_3" runat="server" 
                                                   BackColor="#FFCC99" Font-Names="Courier New" Font-Size="X-Small" Height="58px" 
                                                   Visible="False" Width="364px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_3_Cop" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_3_ID" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_3_Id" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                           
                                           
                                           </td>



                                        </tr>

                                    </table>



                                    <table >
                                    <tr>
                                    <td>
                                    <hr style="height: 4px; width: 1000px" />
                                    </td>
                                    </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label11" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtInicioFuncao4" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label12" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtTerminoFuncao4" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
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
                                                <asp:Label ID="Label13" runat="server" Text="Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtFuncao4" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Funcao4" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label14" runat="server" Text="Setor"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtSetor4" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor4" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label15" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtCargo4" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Cargo4" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            </tr>


                                            <tr>
                                            <td>                                                
                                                <asp:Button ID="cmd_Alterar4" runat="server" Height="19px" 
                                                    onclick="cmd_Alterar4_Click" Text="Alterar Dados" Width="102px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />
                                           </td>
                                           <td>
                                                <asp:Button ID="cmd_Excluir4" runat="server" Height="19px" 
                                                    onclick="cmd_Excluir4_Click" Text="Excluir Classif.Func." Width="162px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />

                                           </td>



                                           <td>
                                           
                                               <asp:Button ID="cmdGHE_4" runat="server" BackColor="#FF3300" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmdGHE_4_Click" Text="Mudar GHE" 
                                                   Width="86px"  Visible="false" />
                                           
                                           </td>

                                           <td>
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_4" runat="server" BackColor="#CCFFFF" 
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="123px" 
                                                   onselectedindexchanged="lst_1_SelectedIndexChanged">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_4" runat="server" Text="lbl_Id_4" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_4" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Add_4_Click" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_4" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Remove_4_Click" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <asp:ListBox ID="lst_Sel_4" runat="server" 
                                                   BackColor="#FFCC99" Font-Names="Courier New" Font-Size="X-Small" Height="58px" 
                                                   Visible="False" Width="364px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_4_Cop" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_4_ID" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_4_Id" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                           
                                           
                                           </td>



                                        </tr>

                                    </table>


                                    <table >
                                    <tr>
                                    <td>
                                    <hr style="height: 4px; width: 1000px" />
                                    </td>
                                    </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label16" runat="server" Text="Início da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtInicioFuncao5" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label17" runat="server" Text="Término da Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDateTimeEditor ID="txtTerminoFuncao5" runat="server" 
                                                    style="text-align: left" Width="100px">
                                                </ig:WebDateTimeEditor>
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
                                                <asp:Label ID="Label18" runat="server" Text="Função"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtFuncao5" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Funcao5" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="style1">
                                                <asp:Label ID="Label19" runat="server" Text="Setor"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtSetor5" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Setor5" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label20" runat="server" Text="Cargo"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtCargo5" runat="server" Width="200px">
                                                </ig:WebTextEditor>
                                                <br />
                                                <asp:DropDownList ID="cmb_Cargo5" runat="server" AutoPostBack="True" 
                                                    Font-Size="X-Small" Height="20px" Width="206px" >
                                                </asp:DropDownList>
                                            </td>
                                            </tr>


                                            <tr>
                                            <td>                                                
                                                <asp:Button ID="cmd_Alterar5" runat="server" Height="19px" 
                                                    onclick="cmd_Alterar5_Click" Text="Alterar Dados" Width="102px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False" />
                                           </td>
                                           <td>
                                                <asp:Button ID="cmd_Excluir5" runat="server" Height="19px" 
                                                    onclick="cmd_Excluir5_Click" Text="Excluir Classif.Func." Width="162px" 
                                                    BackColor="#FF8080" Font-Size="X-Small" Visible="False"/>

                                           </td>



                                           <td>
                                           
                                               <asp:Button ID="cmdGHE_5" runat="server" BackColor="#FF3300" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmdGHE_5_Click" Text="Mudar GHE" 
                                                   Width="86px"  Visible="false" />
                                           
                                           </td>

                                           <td>
                                           
                                           
                                               
                                               <asp:ListBox ID="lst_5" runat="server" BackColor="#CCFFFF" 
                                                   Font-Names="Courier New" Font-Size="X-Small" 
                                                   Width="364px" Visible="False" Height="123px" 
                                                   onselectedindexchanged="lst_1_SelectedIndexChanged">
                                               </asp:ListBox>
                                               <br />
                                               <asp:Label ID="lbl_Id_5" runat="server" Text="lbl_Id_5" Visible="False"></asp:Label>
                                               <asp:Button ID="cmd_Add_5" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Add_5_Click" Text="Add" 
                                                   Width="86px" Visible="False" />
                                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <asp:Button ID="cmd_Remove_5" runat="server" BackColor="#CCCCCC" 
                                                   Font-Size="XX-Small" Height="18px" onclick="cmd_Remove_5_Click" Text="Remove" 
                                                   Width="86px" Visible="False" />
                                               <asp:ListBox ID="lst_Sel_5" runat="server" 
                                                   BackColor="#FFCC99" Font-Names="Courier New" Font-Size="X-Small" Height="58px" 
                                                   Visible="False" Width="364px">
                                               </asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_5_Cop" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_5_ID" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                               <asp:ListBox ID="lst_Sel_5_Id" runat="server" BackColor="#FFCC99" 
                                                   Font-Names="Courier New" Font-Size="X-Small" Height="10px" Visible="False" 
                                                   Width="364px"></asp:ListBox>
                                               <br />
                                           
                                           
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
