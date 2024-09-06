<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaPendencias.aspx.cs"
    Inherits="Ilitera.Net.ListaPendencias" Title="Ilitera.Net" %>

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

    
    <%--       </td>
     
                 </tr>--%>
                    <div class="col-11 subtituloBG">
                <h2 class="subtitulo">Exames e Pendências</h2>
            </div>
            
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
--%><%--       </td>
                  
     
                 </tr>--%>
            <div class="row">
            <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                            <asp:label runat="server" ID="Label2" CssClass="tituloLabel form-label" Text="Colaborador"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:TextBox runat="server" ID="txt_Nome" CssClass="texto form-control form-control-sm"></asp:TextBox>
                        </fieldset>
                    </div>

               
                    <div class="col-md-12 gx-3 gy-4 ms-4">
                        <fieldset>
                     <asp:CheckBox ID="chk_Empresas" runat="server" AutoPostBack="True"
             CssClass="texto form-check-label border-0 bg-transparent gx-4 gy-2 mt-2" Text="Todas Empresas do Grupo" />
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-3 gy-2 ms-4">
                        <fieldset>
            <asp:CheckBox ID="chk_Vencto" runat="server" AutoPostBack="True"
            Cssclass="texto form-check-label border-0 bg-transparent gx-4 gy-2 mt-2" Text="Exames Pendentes" />
                        </fieldset>
                   </div>

                    <div class="col-md-1 gx-2 gy-2 ms-4">
            <asp:TextBox ID="txt_Data1" runat="server" CssClass="texto form-control form-control-sm" AutoPostBack="True"
                     ></asp:TextBox>
                     </div>
               
                  <div class="col-1 gx-2 gy-2 text-center">
                  <asp:Label ID="Label3" runat="server" Font-Bold="False" 
                        Text="a" CssClass="tituloLabel form-label text-center" style="width:15px"></asp:Label>
                  </div>
        

                <div class="col-md-1 gx-2 gy-2">
                        <asp:TextBox ID="txt_Data2" runat="server" CssClass="texto form-control form-control-sm" AutoPostBack="True" 
                        ></asp:TextBox>
                    </div>

                 </div>

          
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                     <%--                    <igtbl:UltraWebGrid ID="UltraWebGridPendencias" runat="server" 
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
--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


            
                  <div class="col-12">
                    <asp:Button ID="cmd_Busca" runat="server" Text="Filtrar" CssClass="btn" Width="95px" onclick="cmd_Busca_Click"/>
                    <asp:Button ID="cmd_CSV" runat="server" Text="Criar CSV" CssClass="btn" Width="95px"  onclick="cmd_CSV_Click"/>
                   </div>



<br />

                 
                      
                 


                      


                        &nbsp;&nbsp;&nbsp; <asp:CheckBox ID="chk_Todos" 
                         runat="server" 
                        Text="Listar todos exames" AutoPostBack="True" Font-Size="X-Small" Visible="False" />

                    <br />
                    <br />

                  <asp:GridView ID="UltraWebGridPendencias" runat="server" Height="460px"  Width="1100px" AutoGenerateColumns="False" CellPadding="4" 
                      EnableModelValidation="True" AllowPaging="True" AllowSorting="True" PageSize="100"  onselectedindexchanged="UltraWebGridPendencias_SelectedIndexChanged"
                      OnPageIndexChanging="UltraWebGridPendencias_PageIndexChanging" Font-Family="Ubuntu">
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:BoundField DataField="Empresa" HeaderText="Empresa">
                          <ControlStyle Width="200px" />
                          </asp:BoundField>
                          <asp:BoundField DataField="Colaborador" HeaderText="Colaborador">
                          <ControlStyle Width="230px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="CPF" HeaderText="CPF">
                          <ControlStyle Width="100px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="GHE" HeaderText="GHE">
                          <ControlStyle Width="240px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="Exame" HeaderText="Exame">
                          <ControlStyle Width="250px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="DtProxima" HeaderText="Dt.Próximo" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="DtUltima" HeaderText="Último Exame" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="Data_Ultimo_Espera" HeaderText="Último Espera" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="60px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="Complementares" HeaderText="Complementares" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="50px"  />
                          </asp:BoundField>
                          <asp:BoundField DataField="UF" HeaderText="UF" ItemStyle-HorizontalAlign="Center">
                          <ControlStyle Width="30px"  />
                          </asp:BoundField>
                      </Columns>
                      <EditRowStyle BackColor="#2461BF" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#D9D9D9" Font-Bold="true" ForeColor="#1C9489" Font-Size="12px" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<RowStyle BackColor="#E1ECEB" />
<SelectedRowStyle BackColor="#F1F1F1" Font-Bold="True" ForeColor="#333333" />
                  </asp:GridView>



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
--%>                    <br />
                    <br />

             <%--       </td>
     
                 </tr>--%>
            </div>
        </div>
   <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>               

</asp:Content>