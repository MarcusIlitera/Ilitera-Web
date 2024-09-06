<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="ListaProcedimentoAPR.aspx.cs"  Inherits="Ilitera.Net.OrdemDeServico.ListaProcedimentoAPR" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .defaultFont
    {
        width: 704px;
    }
        .table
        {}
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
	<div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

        <script language="javascript" src="../scripts/validador.js"></script>
		<script language="javascript">		
		function ConfiguracaoAPR(IdProcedimento, IdEmpresa, IdUsuario)
		{		    
			window.location.href = "CadAPR.aspx?IdProcedimento=" + IdProcedimento + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;
		}
		
		function ProcedimentosMouseOver(gridName, id, objectType)
		{
		    DataGridMouseOverHandler(gridName, id, objectType, 1);
		}
		
		function ProcedimentosMouseOut(gridName, id, objectType)
		{
		    DataGridMouseOutHandler(gridName, id, objectType, 1);
		}
        
        function Procedimentos_CellClickHandler(gridName, cellId, button)
        {
	        if (button == 0)
	        {
	            var cell = igtbl_getCellById(cellId);
	            var IdProcedimentoCell = cell.getPrevCell(true);
	            
	            if (cell.Index == 1)
	                ConfiguracaoAPR(IdProcedimentoCell.getValue(), top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpresa').value, 
	                    top.window.document.frames.conteudo.window.document.getElementById('txtIdUsuario').value);
	        }
        }		
		</script>

			<div class="col-11 subtituloBG" style="padding-top: 10px;">
				<asp:label id="Label6" runat="server" SkinID="BoldFont" CssClass="subtitulo">Mecanismo de Busca para os Procedimentos da Empresa</asp:label>
			</div>

			<div class="col-11 mb-3">
				<div class="row">

					<%-- PALAVRA-CHAVE --%>

					<div class="col-12 gx-3 gy-2">
						<div class="row">
							<div class="col-12">
								<asp:label id="Label10" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Palavra-chave</asp:label>
							</div>

							<div class="col-md-4">
								<asp:textbox id="txtProcedimento" onkeydown="ProcessaEnter(event, 'imgBusca')" runat="server" CssClass="texto form-control form-control-sm"></asp:textbox>
							</div>

							<div class="col-md-1 gx-3">
								<asp:imagebutton id="imgBusca" OnClick="imgBusca_Click" tabIndex="2" runat="server" ImageUrl="Images/search.svg" ToolTip="Procurar" CssClass="btnMenor" style="padding: .4rem;"></asp:imagebutton>
							</div>
						</div>
					</div>

					<%-- FILTROS --%>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label1" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Tipo</asp:label>
						<asp:dropdownlist id="ddlTipoProcedimento" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label13" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Equipamento</asp:label>
						<asp:dropdownlist id="ddlEquipamento" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label4" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Setor</asp:label>
						<asp:dropdownlist id="ddlSetor" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label8" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Produto</asp:label>
						<asp:dropdownlist id="ddlProduto" tabIndex="6" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label12" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Célula</asp:label>
						<asp:dropdownlist id="ddlCelula" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>

					<div class="col-md-4 gx-3 gy-2">
						<asp:label id="Label11" runat="server" SkinID="BoldFontGray" CssClass="tituloLabel form-label">Ferramenta</asp:label>
						<asp:dropdownlist id="ddlFerramenta" runat="server" CssClass="texto form-select"></asp:dropdownlist>
					</div>
				</div>
			</div>



    <eo:Grid ID="UltraWebGridProcedimentos" runat="server" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" GridLines="Both" 
		Height="307px" Width="635px" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdProcedimento" 
		ClientSideOnContextMenu="ShowContextMenu" FullRowMode="False" CssClass="grid" >
            <ItemStyles>
                <eo:GridItemStyleSet>
                    <ItemStyle CssText="background-color: #FAFAFA;" />
                    <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                    <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                    <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 35px !important; background: rgba(28, 148, 137, 0.80); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                    <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                    <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:GridItemStyleSet>
             </ItemStyles>
             <ColumnHeaderStyle CssClass="tabelaC colunas" />
            <Columns>

                <eo:StaticColumn HeaderText="Número" AllowSort="True" 
                    DataField="Numero" Name="Numero" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="100" >
                   <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                </eo:StaticColumn>
               
                <eo:StaticColumn HeaderText="Nome" AllowSort="True" 
                    DataField="Nome" Name="Nome" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="500" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
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
        </eo:Grid>

			<div class="col-11 gx-3 gy-2">
				<asp:label id="lblTotalRegistros" runat="server"></asp:label>
			</div>


<%--						<igtbl:ultrawebgrid id="UltraWebGridProcedimentos" runat="server" Height="164px" Width="602px" ImageDirectory="/ig_common/Images/" onpageindexchanged="UltraWebGridProcedimentos_PageIndexChanged"><Bands>
<igtbl:UltraGridBand GroupByRowDescriptionMask="[caption] : [value] ([count])   --   Relat&#243;rio: &lt;a href=&quot;javascript:RelatorioN('[value]')&quot;&gt;Por Nome&lt;/a&gt; ou &lt;a href=&quot;javascript:RelatorioD('[value]')&quot;&gt;Por Data&lt;/a&gt;"><Columns>
<igtbl:UltraGridColumn Key="IdProcedimento" AllowGroupBy="No" BaseColumnName="IdProcedimento" Hidden="True" Width="0px">
<Header Caption="IdProcedimento"></Header>

<Footer Key="Id"></Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Numero" AllowGroupBy="No" BaseColumnName="Numero" FooterText="" Format="" Width="100px" EditorControlID="">
<CellButtonStyle HorizontalAlign="Center" Cursor="Hand" BackColor="#EFFFF6" BorderWidth="0px" BorderStyle="None" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" Font-Underline="True" ForeColor="#156047"></CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleLeft="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Center" Font-Bold="True">
<BorderDetails StyleLeft="None"></BorderDetails>
</CellStyle>

<SelectedCellStyle BorderStyle="None"></SelectedCellStyle>

<Header Caption="N&#250;mero POPs">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Header>

<Footer Caption="" Key="NomeAbreviado">
<RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
<igtbl:UltraGridColumn Key="Nome" AllowGroupBy="No" BaseColumnName="Nome" FooterText="" Format="" Width="500px" EditorControlID="">
<CellButtonStyle HorizontalAlign="Left" Cursor="Hand" BackColor="#EFFFF6" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047">
<Padding Left="3px"></Padding>

<BorderDetails StyleLeft="None" StyleRight="None" StyleTop="None" StyleBottom="None"></BorderDetails>
</CellButtonStyle>

<HeaderStyle>
<BorderDetails StyleRight="None" StyleTop="None"></BorderDetails>
</HeaderStyle>

<CellStyle HorizontalAlign="Left">
<Padding Left="3px"></Padding>

<BorderDetails StyleRight="None"></BorderDetails>
</CellStyle>

<Header Caption="Nome do Procedimento">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Header>

<Footer Caption="" Key="NomeCompleto">
<RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
</Footer>
</igtbl:UltraGridColumn>
</Columns>

<RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
<BorderDetails WidthLeft="3px" WidthTop="3px" WidthRight="3px" WidthBottom="3px"></BorderDetails>
</RowTemplateStyle>

<AddNewRow Visible="NotSet" View="NotSet"></AddNewRow>
</igtbl:UltraGridBand>
</Bands>

<DisplayLayout Version="3.00" Name="UltraWebGridProcedimentos" RowHeightDefault="18px" TableLayout="Fixed" ViewType="OutlookGroupBy" RowSelectorsDefault="No" AutoGenerateColumns="False">
<FrameStyle BackColor="WhiteSmoke" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Height="164px" Width="602px"></FrameStyle>



<Images  ImageDirectory="/ig_common/Images/"></Images>

<ClientSideEvents CellClickHandler="Procedimentos_CellClickHandler" MouseOverHandler="ProcedimentosMouseOver" MouseOutHandler="ProcedimentosMouseOut"></ClientSideEvents>

<Pager PageSize="7" AllowPaging="True" StyleMode="QuickPages" Alignment="Center" PrevText="Anterior" NextText="Pr&#243;ximo" QuickPages="10" Pattern="P&#225;gina &lt;b&gt;[currentpageindex]&lt;/b&gt; de &lt;b&gt;[pagecount]&lt;/b&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;b&gt;[default]&lt;/b&gt;">
<PagerStyle HorizontalAlign="Center" BackColor="#DEEFE4" BorderWidth="0px" BorderStyle="None" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></PagerStyle>
</Pager>

<EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid">
<BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px"></BorderDetails>
</FooterStyleDefault>

<HeaderStyleDefault HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#DEEFE4" Font-Bold="True" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#44926D" Height="18px"></HeaderStyleDefault>

<RowStyleDefault HorizontalAlign="Center" VerticalAlign="Middle" BackColor="White" BorderColor="#7CC5A1" BorderWidth="1px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="XX-Small" ForeColor="#156047"></RowStyleDefault>

<GroupByRowStyleDefault Height="18px"></GroupByRowStyleDefault>

<GroupByBox Hidden="True"></GroupByBox>

<AddNewBox>
<BoxStyle BackColor="LightGray" BorderWidth="1px" BorderStyle="Solid"></BoxStyle>
</AddNewBox>

<ActivationObject AllowActivation="False" BorderColor="124, 197, 161" BorderWidth=""></ActivationObject>
</DisplayLayout>
</igtbl:ultrawebgrid>
--%>
			<div class="col-11 text-center">
				<asp:hyperlink id="lisTodas" runat="server" SkinID="BoldLink" CssClass="btn">Lista Todas</asp:hyperlink>
				<asp:linkbutton id="lkbListagemPOPs" onclick="lkbListagemPOPs_Click" runat="server" SkinID="BoldLinkButton" CssClass="btn">Listagem por nPOPS</asp:linkbutton>
				<asp:linkbutton id="lkbListagemNome" onclick="lkbListagemNome_Click" runat="server" SkinID="BoldLinkButton" CssClass="btn"> Listagem por Nome</asp:linkbutton>
			</div>
			
			
			<asp:Image id="Image1" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image6" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image2" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image3" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image4" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image5" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image7" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image8" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image9" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Image id="Image10" runat="server" ImageUrl="img/5pixel.gif" Visible="false"></asp:Image>
			<asp:Label ID="lbl_Id_Empresa" runat="server" Text="IdEmpresa" Visible="False"></asp:Label>
			<asp:Label ID="lbl_Id_Usuario" runat="server" Text="IdUsuario" Visible="False"></asp:Label>

         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

		</div>
	</div>
</asp:Content>


