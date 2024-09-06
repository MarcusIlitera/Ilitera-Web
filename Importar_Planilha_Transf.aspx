<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Importar_Planilha_Transf.aspx.cs"
    Inherits="Ilitera.Net.Importar_Planilha_Transf" Title="Ilitera.Net" %>

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
    
.boldFont
{
	font: Bold xx-small Verdana;
	color:#44926D;
}
           .auto-style1 {
               margin-bottom: 0px;
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"  >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">
            <div class="col-11 subtituloBG mb-2">
                <h2 class="subtitulo">Importar Planilha - Transferência / Nova Classificação Funcional</h2>
            </div>

            <div class="col-md-6" style="margin-left: 10px; margin-bottom: 30px;">
                <asp:Label ID="lbl_Selecionar" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Selecione planilha para Importação</asp:Label>
                <asp:Literal runat="server"><br /></asp:Literal>
                <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" class="texto form-control form-control-sm" Font-Size="XX-Small" Width="517px"  />
                <asp:Literal runat="server"><br /></asp:Literal>
                
                    <asp:Button ID="cmd_Analisar" onclick="cmd_Busca_Click"  runat="server" Text="Analisar Planilha" CssClass="btn"/>
                
            </div>


                <asp:TextBox ID="txt_Status" runat="server" Visible="False"></asp:TextBox>


                <asp:Label ID="lbl_Arq" runat="server" Font-Bold="True" Font-Size="XX-Small" ForeColor="#993300" Text="Label" Visible="False"></asp:Label>


                <asp:Label ID="lbl_Id_Importacao" runat="server" Font-Size="XX-Small" Text="Label" Visible="False"></asp:Label>

                <asp:Label ID="lbl_Formato" runat="server" Font-Size="XX-Small" Text="MM/dd/yyyy" Visible="False"></asp:Label>


                                            <br />
                <asp:Label ID="lbl_Planilhas" runat="server" Font-Bold="True" Text="Planilhas:" Enabled="False" Font-Size="Small" Visible="False"></asp:Label>
                <asp:DropDownList ID="cmb_Planilha" runat="server" AutoPostBack="True" Font-Size="XX-Small" Height="16px" Visible="False" Width="113px">
                </asp:DropDownList>


                <br />
                <asp:Button ID="btn_Cabecalho" runat="server" BackColor="#99CCFF" BorderStyle="None" CssClass="auto-style1" Height="18px" Visible="False" Width="21px" />
                <asp:Label ID="lbl_Cabecalho" runat="server" Font-Bold="True" Text="Cabeçalho" Enabled="False" Font-Size="Small" Visible="False"></asp:Label>
                    
                <asp:Button ID="btn_Dados" runat="server" BackColor="#FFFF99" BorderStyle="None" CssClass="auto-style1" Height="18px" Visible="False" Width="21px" />
                <asp:Label ID="lbl_Dados" runat="server" Font-Bold="True" Text="Dados" Enabled="False" Font-Size="Small" Visible="False"></asp:Label>
                    
                <asp:Button ID="btn_Inconsistencias" runat="server" BackColor="#FF9999" BorderStyle="None" CssClass="auto-style1" Height="18px" Visible="False" Width="21px" />
                <asp:Label ID="lbl_Incons" runat="server" Font-Bold="True" Text="Inconsistências" Enabled="False" Font-Size="Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Data1" runat="server" Font-Size="X-Small" Visible="False" Font-Bold="True">Data Planilha:</asp:Label>
                <asp:Label ID="lblC_Data" runat="server" Font-Size="X-Small" Visible="False" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblC_Login1" runat="server" Font-Size="X-Small" Visible="False" Font-Bold="True">Usuário:</asp:Label>
                <asp:Label ID="lblC_Login" runat="server" Font-Size="X-Small" Visible="False" Font-Bold="True"></asp:Label>

                <asp:Button ID="cmd_Importar" onclick="cmd_CSV_Click"  runat="server" Text="Realizar Importação" Font-Size="X-Small" 
                    BackColor="#FF8080" Font-Bold="True" 
                    Width="120px" Enabled="False" Visible="False" />

                <asp:Button ID="cmd_Cancelar" onclick="cmd_Cancelar_Processo_Click"  runat="server" Text="Cancelar Processo" Font-Size="X-Small" 
                    BackColor="#FF8080" Font-Bold="True" 
                    Width="120px" Enabled="False" Visible="False" />

                <asp:Button ID="cmd_Exibir_Log" onclick="cmd_Exibir_Log_Click"  runat="server" Text="Exibir Log" Font-Size="X-Small" 
                    BackColor="#FF8080" Font-Bold="True" 
                    Width="120px" Visible="False" />



                <asp:Label ID="lblLINHA" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_CPF" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_FUNCAO" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_SETOR" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_CNPJ_Origem" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_CNPJ_Destino" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Filial_Origem" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Filial_Destino" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Data_Inicial" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Dia_Data_Inicial" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Inativar_Origem" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Data_Demissao" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_Dia_Data_Demissao" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>
                <asp:Label ID="lblC_GHE" runat="server" Font-Size="XX-Small" Visible="False"></asp:Label>


                <asp:Label ID="lbl_Inconsistencias" runat="server" Font-Bold="False" 
                    Text="Inconsistências:" Font-Size="X-Small" Visible="False"></asp:Label>
                <br />
                <asp:ListBox ID="lst_Inconsistencias" OnSelectedIndexChanged="lst_Inconsistencias_SelectedIndexChanged"  runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="690px" Font-Bold="True" ForeColor="#CC0000"></asp:ListBox>


                <asp:Label ID="lbl_Processamento" runat="server" Font-Bold="False" 
                    Text="Resultado do Processamento" Font-Size="X-Small" Visible="False"></asp:Label>
                <br />
                <asp:ListBox ID="lst_Processamento" OnSelectedIndexChanged="lst_Inconsistencias_SelectedIndexChanged"  runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="690px" Font-Bold="True" ForeColor="#0000CC" Height="118px"></asp:ListBox>
                <br />


    
                <asp:GridView ID="grvData" OnSelectedIndexChanged="grvData_SelectedIndexChanged" runat="server" >
                    <SelectedRowStyle BackColor="#FFFFCC"  />
                </asp:GridView>
    


                </span><br /><asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Filtros : " Visible="False"></asp:Label>
                <asp:Label ID="Label2" runat="server" Font-Bold="False" 
                    Text="Localizar texto:" Font-Size="X-Small" Visible="False"></asp:Label>
                <asp:TextBox ID="txt_Nome" runat="server" AutoPostBack="True" 
                    BackColor="#CCCCCC" CausesValidation="True" Height="16px" MaxLength="30"                         
                    Width="96px" Font-Size="XX-Small" Visible="False"></asp:TextBox>
                <asp:Button ID="cmd_Localizar" OnClick="cmd_Localizar_Click"  runat="server" Font-Size="XX-Small" Text="Localizar" Visible="False" />



                <asp:Label ID="Label8" runat="server" Font-Bold="False" 
                    Text="Ir para linha:" Font-Size="X-Small" Visible="False"></asp:Label>
                <asp:TextBox ID="txt_Linha" runat="server" AutoPostBack="True" 
                    BackColor="#CCCCCC" CausesValidation="True" Height="16px" MaxLength="4"                         
                    Width="52px" Font-Size="XX-Small" Visible="False"></asp:TextBox>
                <asp:Button ID="cmd_Linha" OnClick="cmd_Linha_Click"  runat="server" Font-Size="XX-Small" Text="Ir para" Visible="False" />




<%--                    <igtbl:UltraWebGrid ID="UltraWebGridPendencias" runat="server" 
                        Height="460px" 
                        OnInitializeRow="UltraWebGridPendencias_InitializeRow" 
                        OnPageIndexChanged="UltraWebGridPendencias_PageIndexChanged" 
                        style="text-align: justify" Width="800px">
                        <displaylayout autogeneratecolumns="False" name="UltraWebGridListaExtintores" 
                            rowheightdefault="18px" rowselectorsdefault="No" tablelayout="Fixed" 
                            version="3.00" ViewType="OutlookGroupBy">
                            <addnewbox hidden="False">
                            </addnewbox>
                            <pager alignment="Center" allowpaging="True" changelinkscolor="True" 
                                nexttext="Próximo" PageSize="1000" 
                                pattern="Página &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;" 
                                prevtext="Anterior" quickpages="10" stylemode="QuickPages">
                                <PagerStyle BackColor="#DEEFE4" BorderStyle="None" BorderWidth="0px" 
                                    Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px" />
                            </pager>
                            <headerstyledefault backcolor="#DEEFE4" font-bold="True" font-names="Verdana" 
                                font-size="XX-Small" forecolor="#44926D" horizontalalign="Center" 
                                verticalalign="Middle">
                                <Margin Bottom="0px" Left="0px" Right="0px" Top="0px" />
                                <Padding Bottom="0px" Left="0px" Right="0px" Top="0px" />
                            </headerstyledefault>
                            <groupbyrowstyledefault backcolor="Control" bordercolor="Window">
                            </groupbyrowstyledefault>
                            <framestyle backcolor="WhiteSmoke" bordercolor="#7CC5A1" borderstyle="Solid" 
                                borderwidth="1px" font-names="Verdana" font-size="XX-Small" height="460px" 
                                width="800px">
                            </framestyle>
                            <footerstyledefault backcolor="LightGray" borderstyle="Solid" borderwidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                    WidthTop="1px" />
                            </footerstyledefault>
                            <activationobject allowactivation="False" bordercolor="124, 197, 161" 
                                borderstyle="Solid" borderwidth="1px">
                            </activationobject>
                            <groupbybox hidden="False">
                                <boxstyle backcolor="ActiveBorder" bordercolor="Window">
                                </boxstyle>
                            </groupbybox>
                            <editcellstyledefault borderstyle="None" borderwidth="0px">
                            </editcellstyledefault>
                            <rowstyledefault backcolor="White" bordercolor="#7CC5A1" borderstyle="Solid" 
                                borderwidth="1px" font-names="Verdana" font-size="XX-Small" forecolor="#156047" 
                                horizontalalign="Center">
                            </rowstyledefault>
                            <filteroptionsdefault>
                                <filteroperanddropdownstyle backcolor="White" bordercolor="Silver" 
                                    borderstyle="Solid" borderwidth="1px" customrules="overflow:auto;" 
                                    font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px">
                                    <Padding Left="2px" />
                                </filteroperanddropdownstyle>
                                <filterhighlightrowstyle backcolor="#151C55" forecolor="White">
                                </filterhighlightrowstyle>
                                <filterdropdownstyle backcolor="White" bordercolor="Silver" borderstyle="Solid" 
                                    borderwidth="1px" customrules="overflow:auto;" 
                                    font-names="Verdana,Arial,Helvetica,sans-serif" font-size="11px" height="300px" 
                                    width="200px">
                                    <Padding Left="2px" />
                                </filterdropdownstyle>
                            </filteroptionsdefault>
                            <ClientSideEvents CellClickHandler="ListaExtintores_CellClickHandler" 
                                MouseOutHandler="ListaExtintoresMouseOut" 
                                MouseOverHandler="ListaExtintoresMouseOver" />
>                        </displaylayout>
                        <bands>
                            <igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relatório: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;">                            
                            <igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])">                            
                                <Columns>
                                    <igtbl:UltraGridColumn AllowGroupBy="Yes" BaseColumnName="Empresa" 
                                        EditorControlID="" FooterText="" Format="" Key="Empresa" Width="200px">
                                        <cellstyle font-bold="False" horizontalalign="Left" Font-Size="XX-Small">
                                            <Padding Left="3px" />
                                            <BorderDetails StyleLeft="None" />
                                        </cellstyle>
                                        <footer caption="">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </footer>
                                        <HeaderStyle>
                                        <BorderDetails StyleLeft="None" StyleTop="None" />
                                        </HeaderStyle>
                                        <header caption="Empresa">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn AllowGroupBy="Yes" BaseColumnName="Colaborador" 
                                        EditorControlID="" FooterText="" Format="" Key="Colaborador" Width="230px">
                                        <cellstyle horizontalalign="Center" Font-Size="XX-Small">
                                            <Padding Left="3px" />
                                            <BorderDetails StyleRight="None" />
                                        </cellstyle>
                                        <footer caption="">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </footer>
                                        <HeaderStyle>
                                        <BorderDetails StyleRight="None" StyleTop="None" />
                                        </HeaderStyle>
                                        <header caption="Colaborador">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn AllowGroupBy="Yes" BaseColumnName="GHE" 
                                        EditorControlID="" FooterText="" Format="" Key="GHE" Width="240px">
                                        <cellstyle horizontalalign="Left" Font-Size="XX-Small">
                                            <Padding Left="3px" />
                                            <BorderDetails StyleRight="None" />
                                        </cellstyle>
                                        <footer caption="">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </footer>
                                        <HeaderStyle>
                                        <BorderDetails StyleRight="None" StyleTop="None" />
                                        </HeaderStyle>
                                        <header caption="GHE">
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn AllowGroupBy="Yes" BaseColumnName="Exame" 
                                        EditorControlID="" FooterText="" Format="" Key="Exame" Width="250px">
                                        <cellstyle horizontalalign="Center" Font-Size="XX-Small">
                                            <Padding Left="3px" />
                                            <BorderDetails StyleRight="None" />
                                        </cellstyle>
                                        <footer caption="">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </footer>
                                        <HeaderStyle>
                                        <BorderDetails StyleRight="None" StyleTop="None" />
                                        </HeaderStyle>
                                        <header caption="Exame">
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </header>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn AllowGroupBy="Yes" BaseColumnName="DtProxima" 
                                        EditorControlID="" FooterText="" Format="" Key="DtProxima" Width="82px">
                                        <cellstyle horizontalalign="Center" Font-Size="XX-Small">
                                            <Padding Left="3px" />
                                            <BorderDetails StyleRight="None" />
                                        </cellstyle>
                                        <footer caption="">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </footer>
                                        <HeaderStyle>
                                        <BorderDetails StyleRight="None" StyleTop="None" />
                                        </HeaderStyle>
                                        <header caption="Vencimento">
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </header>
                                    </igtbl:UltraGridColumn>
                                </Columns>
                                <rowtemplatestyle backcolor="White" bordercolor="White" borderstyle="Ridge">
                                    <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                        WidthTop="3px" />
                                </rowtemplatestyle>
                                <addnewrow view="NotSet" visible="NotSet">
                                </addnewrow>
                            </igtbl:UltraGridBand>
                        </bands>
                    </igtbl:UltraWebGrid>
--%>                    

             <%--       </td>
     
                 </tr>--%>
   <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>    


        </div>        
    </div>
</asp:Content>