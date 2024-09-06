<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="visualizarDadosEmpresa.aspx.cs" Inherits="Ilitera.Net.visualizarDadosEmpresa" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
       <style type="text/css">
        .linha
   {
	font: 8px Verdana, Arial, Helvetica, sans-serif, Tahoma;
    }
           .btnLogarClass
           {}
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
     <div class="row gx-3 gy-2 w-100">

         <div class="col-11 subtituloBG" style="padding-top: 10px" >
            <asp:Label runat="server" class="subtitulo">Dados da Empresa</asp:Label>
        </div>

         <div class="col-11 mb-3">
             <div class="row">

                 <%-- LINHA UM --%>

                 <div class="col-md-4 gx-3 gy-2">
                     <asp:Label ID="lblNomeEmpresa" runat="server" CssClass="tituloLabel form-label" Text="Nome"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtNomeEmpresa" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-4 gx-3 gy-2">
                     <asp:Label ID="lblRazaoSocial" runat="server" CssClass="tituloLabel form-label" Text="Razão Social"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtRazaoSocial" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblCodigo" runat="server" Text="Código" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCodigo" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblDataCadastro" runat="server" Text="Data" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtDataCadastro" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <%-- LINHA DOIS --%>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblGrupo" runat="server" Text="Grupo" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtGrupo" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-1 gx-3 gy-2">
                     <asp:Label ID="lblTipoLogradouro" runat="server" Text="Tipo Logr." CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtTipoLogradouro" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-4 gx-3 gy-2">
                     <asp:Label ID="lblLogradouro" runat="server" Text="Logradouro" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtLogradouro" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-1 gx-3 gy-2">
                     <asp:Label ID="lblNumeroEndereco" runat="server" Text="Número" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtNumeroEndereco" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblComplemento" runat="server" Text="Complemento" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtComplemento" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                  <%-- LINHA TRÊS --%>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblBairro" runat="server" Text="Bairro" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtBairro" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblCep" runat="server" Text="CEP" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCep" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblCidade" runat="server" Text="Cidade" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCidade" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblEstado" runat="server" Text="Estado" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtEstado" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-1 gx-3 gy-2">
                     <asp:Label ID="lblNumeroEmpregados" runat="server" Text="Nº Empr." CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtNumeroEmpregados" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <%-- LINHA QUATRO --%>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblCnpj" runat="server" Text="CNPJ / CEI" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCnpj" runat="server" ReadOnly="true" CssClass="texto form-control form-controls-m"></asp:textbox>
                 </div>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblIe" runat="server" Text="I.E." CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtIe" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-2 gx-3 gy-2">
                     <asp:Label ID="lblCCM" runat="server" Text="CCM" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCCM" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblAtividade" runat="server" Text="Atividade" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtAtividade" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblCnae" runat="server" Text="CNAE" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtCnae" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <%-- LINHA CINCO --%>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label id="lblSite" runat="server" Text="Site" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtSite" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblEmail" runat="server" Text="E-mail" CssClass="tituloLabel form-label"></asp:Label>
                     
                     <asp:textbox ID="txtEmail" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblMotorista" runat="server" Text="Motorista" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtMotorista" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <div class="col-md-3 gx-3 gy-2">
                     <asp:Label ID="lblDiretor" runat="server" Text="Diretor" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtDiretor" runat="server" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>

                 <%-- LINHA SEIS --%>

                 <div class="col-md-9 gx-3 gy-2">
                     <asp:Label ID="lblObservacao" runat="server" Text="Observação" CssClass="tituloLabel form-label"></asp:Label>
                     <asp:Literal runat="server"><br /></asp:Literal>
                     <asp:textbox ID="txtObservacao" runat="server" TextMode="MultiLine" ReadOnly="true" CssClass="texto form-control form-control-sm"></asp:textbox>
                 </div>
             
             </div>
         </div>

   <br />
                    <table width="790px">

                      

                    
                   <%-- <ig:WebDataGrid runat="server" ID="gridContatoTelefonico" AutoGenerateColumns="false" DataKeyFields="IdContatoTelefonico"
                        width="590" Height="200" CellSpacing="0">
                        <Columns>
                            <ig:BoundDataField DataFieldName="DDD" Key="DDD" Header-Text="DDD"></ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Numero" Key="Numero" Header-Text="Número"></ig:BoundDataField>                            
                            <ig:BoundDataField DataFieldName="Nome" Key="Nome" Header-Text="Nome"></ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Departamento" Key="Departamento" Header-Text="Departamento"></ig:BoundDataField>
                        </Columns>
                    </ig:WebDataGrid>--%>
                    </table>

    <table width="1050px" class="mb-3">
        <tr>
            <td>
                    
      <eo:grid ID="gridContatoTelefonico" runat="server" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404" GridLines="Both" Height="138px" Width="1050px" ColumnHeaderDividerOffset="6" 
                ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdContatoTelefonico" CssClass="grid" >
            <ItemStyles>
                <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: #FAFAFA;" />
                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                </eo:GridItemStyleSet>
            </ItemStyles>
          <ColumnHeaderStyle CssClass="tabelaC colunas" />
            <Columns>
                <eo:StaticColumn HeaderText="DDD" AllowSort="True" 
                    DataField="DDD" Name="DDD" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="80">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Número" AllowSort="True" 
                    DataField="Numero" Name="Numero" ReadOnly="True" Width="210">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn AllowSort="True" DataField="Nome" 
                    HeaderText="Nome" Name="Nome" ReadOnly="True" 
                    Width="350">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn AllowSort="True" DataField="Departamento" 
                    HeaderText="Departamento" Name="Departamento" ReadOnly="True" 
                    Width="410">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>

            </Columns>
            <ColumnTemplates>
                <eo:TextBoxColumn>
                    <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                </eo:TextBoxColumn>
                <eo:DateTimeColumn>
                    <DatePicker ControlSkinID="None" DayCellHeight="16" DayCellWidth="19" 
                        DayHeaderFormat="FirstLetter" DisabledDates="" OtherMonthDayVisible="True" 
                        SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL" 
                        TitleRightArrowImageUrl="DefaultSubMenuIcon">
                        <PickerStyle CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                        <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                        <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                        <TitleArrowStyle CssText="cursor:hand" />
                        <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                        <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                        <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                        <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                        <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                    </DatePicker>
                </eo:DateTimeColumn>
                <eo:MaskedEditColumn>
                    <MaskedEdit ControlSkinID="None" 
                        TextBoxStyle-CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                    </MaskedEdit>
                </eo:MaskedEditColumn>
            </ColumnTemplates>
            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
        </eo:grid>

        </td>
        </tr>
</table>

         <div class="col-11">
             <div class="row">
                 <div class="col-md-6 gx-3 gy-2">
                    <p class="tituloLabel form-label">Primeiro Preposto da Empresa</p>
                    <asp:DropDownList ID="cmb_Preposto" runat="server" CssClass="texto form-select form-select-sm mb-2" AutoPostBack="True" ></asp:DropDownList>
                </div>

                <div class="col-md-6 gx-3 gy-2">
                    <p class="tituloLabel form-label">Segundo Preposto da Empresa</p>
                    <asp:DropDownList ID="cmb_Preposto2" runat="server" CssClass="texto form-select form-select-sm mb-2"  AutoPostBack="True" ></asp:DropDownList>
                </div>

                 <div class="gx-3 gy-2 mb-3">
                    <asp:Button ID="cmd_Confirmar" runat="server" CssClass="btn" Text="Confirmar Preposto(s)" onclick="cmd_Confirmar_Click"  />
                </div>
             </div>
         </div>

                
                
                
                
                

                <div class="col-11 subtituloBG" style="padding-top: 10px" >
                    <asp:Label runat="server" class="subtitulo">Cadastrar Preposto</asp:Label>
                </div> 
                
                <div class="col-11">
                    <div class="row">
                        <div class="col-md-6 gx-3 gy-2 mb-2">
                            <asp:Label ID="Label1" runat="server" Text="Nome" CssClass="tituloLabel form-label"></asp:Label>
                            <asp:textbox ID="txt_Nome"  MaxLength="50" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                        </div>

                        <div class="col-md-3 gx-3 gy-2">
                            <asp:Label ID="Label2" runat="server" Text="CPF" CssClass="tituloLabel form-label"></asp:Label>
                            <asp:textbox ID="txt_NIT"  MaxLength="20" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
                        </div>

                        <div class="col-12 gx-3 gy-2">
                            <asp:Button ID="cmd_Salvar" runat="server" Text="Salvar Registro" Width="118px" CssClass="btn" onclick="cmd_Salvar_Click" />
                        </div>
                    </div>
                </div>
                    
                        
                                
                         <tr>
                            <td align="center">


                                
                            </td>
                        </tr>

                                    
                    </table>

             
            </div>
        </div>
</asp:Content>
