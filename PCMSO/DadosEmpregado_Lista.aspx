<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site.Master" CodeBehind="DadosEmpregado_Lista.aspx.cs"  Inherits="Ilitera.Net.DadosEmpregado_Lista" Title="Ilitera.Net" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
        <link href="css/forms.css" rel="stylesheet" type="text/css" />
        <link href="css/tabelas.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .defaultFont
    {
        width: 627px;
    }
        #Table4
        {
            width: 594px;
        }
        #Table15
        {
            width: 214px;
        }
        #Table20
        {
            width: 184px;
        }
        #Table24
        {
            width: 333px;
        }
        #Table27
        {
            width: 583px;
        }
        .labelDadosOpcao {
            color: #1C9489;
            font-size: 12px;
            font-family: 'Ubuntu';
            font-weight: 500;
        }  
        .dadoDadosOpcao {
            color: #B0ABAB;
            font-size: 12px;
            font-family: 'Ubuntu';
            font-weight: 500;
        }  
        .dados {
            background: #FFFFFF;
            border: 1px solid #B0ABAB;
            border-radius: 4px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">
    <script language="javascript">
        var g_DatePicker = null;
        function datepicker_loaded(obj) {
            g_DatePicker = obj;
        }
        //Check whether "Back Ordered" column is checked
        function is_back_ordered(item) {

            return item.getCell(1).getValue() == "1";
        }
        //This function is called when user checks/unchecks
        //"back ordered" column
        function end_edit_back_ordered(cell, newValue) {
            //Get the GridItem object
            var item = cell.getItem();

            //Force update the "Available On" cell. Note
            //the code must be delayed with a timer so that
            //the value of the "Back Ordered" cell is first
            //updated
            setTimeout(function () {
                var availOnCell = item.getCell(2);
                availOnCell.refresh();
            }, 10);
            //Accept the new value
            return newValue;
        }
        //This function is called for "Available On" column
        //to translate a cell value to the final HTML to be
        //displayed in the cell. You can put any HTML inside
        //the grid cell even though this function only returns
        //simple text. For example, instead of returning
        //"N/A", you can return something like this:
        // <input type="text" disabled="disabled" style="width:100px" />
        //This would render a disabled textbox inside the Grid cell
        //when no value should be entered for "available On"
        //column. Note that the textbox rendered by this 
        //function, regardless disabled or not, are never
        //used by the user because the actual editing UI is
        //specified by the column's EditorTemplate, which is
        //a DatePicker control for this sample
        function get_avail_on_text(column, item, cellValue) {
            if (!is_back_ordered(item)) {

                if (document.getElementById("<%=lblnreg.ClientID%>").value != "") {
                    document.getElementById("<%=lblnreg.ClientID%>").value = document.getElementById("<%=lblnreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";
                }
                return "N/A";
            }
            else {
                if (!cellValue) {
                    document.getElementById("<%=lblreg.ClientID%>").value = document.getElementById("<%=lblreg.ClientID%>").value + "|" + item.getCell(5).getValue() + " ";
                    if (document.getElementById("<%=lblnreg.ClientID%>").value == "") document.getElementById("<%=lblnreg.ClientID%>").value = "|";
                    return "Click to edit";
                }
                else
                    return g_DatePicker.formatDate(cellValue, "MM/dd/yyyy");
            }
        }
        //This function is called when user clicks any cell in 
        //the "Available On" column. 
        function begin_edit_avail_on_date(cell) {
            //Get the item object
            var item = cell.getItem();
            //Do not enter edit mode unless "back ordered" is checked
            if (!is_back_ordered(item))
                return false;
            //Load cell value into the DatePicker control
            var v = cell.getValue();
            g_DatePicker.setSelectedDate(v);
        }
        //This function is called when user leaves edit mode
        //from an "Available On" cell. It returns the DatePicker
        //value to the Grid
        function end_edit_avail_on_date(cell) {
            return g_DatePicker.getSelectedDate();
        }
        function OnItemCommand(grid, itemIndex, colIndex, commandName) {
            //grid.raiseItemCommandEvent(itemIndex, commandName);
            grid.raiseItemCommandEvent(itemIndex, colIndex.toString());
        }
    </script>





  


<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>	
            
            <div class="col-11 mb-3">
                <div class="row">
                    <%-- LINHA 1--%>
                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                            <asp:label runat="server" ID="lblNome" CssClass="tituloLabel form-label" Text="Nome"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorNome" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblTipoBene" CssClass="tituloLabel form-label" Text="Tipo de Beneficiário"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorBene" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDataNascimento" CssClass="tituloLabel form-label" Text="Nascimento"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorNasc" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-1 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblIdade" CssClass="tituloLabel form-label" Text="Idade"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorIdade" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblSexo" CssClass="tituloLabel form-label" Text="Sexo"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorSexo" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <%-- LINHA 2 --%>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblRegistro" CssClass="tituloLabel form-label" Text="RE"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorRegistro" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblRegRev" CssClass="tituloLabel form-label" Text="Regime Revezamento"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorRegRev" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-3 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblJornada" CssClass="tituloLabel form-label" Text="Jornada"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorJornada" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblAdmissao" CssClass="tituloLabel form-label" Text="Admissão"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorAdmissao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDemissao" CssClass="tituloLabel form-label" Text="Demissão"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorDemissao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <%-- LINHA 3 --%>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblDataIni" CssClass="tituloLabel form-label" Text="Início Função"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorDataIni" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-2 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblGHE" CssClass="tituloLabel form-label" Text="Tempo de Empresa"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorTempoEmpresa" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblSetor" CssClass="tituloLabel form-label" Text="Setor"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorSetor" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                    <div class="col-md-4 gx-2 gy-2">
                        <fieldset>
                             <asp:label runat="server" ID="lblFuncao" CssClass="tituloLabel form-label" Text="Função"></asp:label>
                            <asp:Literal runat="server"><br /></asp:Literal>
                            <asp:label runat="server" ID="lblValorFuncao" CssClass="texto form-control form-control-sm"></asp:label>
                        </fieldset>
                    </div>

                </div>
            </div>

				<tr>
					<!--<td nowrap align="center">
						<table class="fotoborder" bgcolor="white" id="Table2" cellspacing="0" 
                            cellpadding="0" align="center"
							border="0">
							<tr> 
								<td class="normalFont" width="490" bgcolor="#EDFFEB">
									
									<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE class="tableEdit" id="Table6" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR class="tableEdit" vAlign="middle">
											<TD class="tableEdit" vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="tableEdit" vAlign="middle" align="right" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="105" bgColor="#edffeb">
                                                </TD>
											<TD align="left" width="56" bgColor="#edffeb">
												<TABLE id="Table7" cellSpacing="0" cellPadding="0" width="30" border="0">
													<TR>
														
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="64" bgColor="#edffeb">
                                                &nbsp; 
                                                 </TD>
											<TD align="left" width="98" bgColor="#edffeb">
												<TABLE id="Table8" cellSpacing="0" cellPadding="0" width="76" border="0">
													<TR>
														<TD class="textDadosCampo" width="97" bgColor="#edffeb">
                                                            </TD>
													</TR> 
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="32" bgColor="#edffeb">
                                                
                                                </TD>
											<TD align="left" width="34" bgColor="#edffeb">
												<TABLE id="Table9" cellSpacing="0" cellPadding="0" width="22" border="0">
													<TR>
														<TD class="textDadosCampo" width="27" bgColor="#edffeb">
                                                            
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="27" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="60" bgColor="#edffeb">
												<TABLE id="Table10" cellSpacing="0" cellPadding="0" width="59" border="0">
													<TR>
														<TD class="textDadosCampo" width="58" bgColor="#edffeb">
											</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table11" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table12" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                &nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="19" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table13" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" width="95" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											    </TD>
											<TD class="textDadosOpcao" align="left" width="111" bgColor="#edffeb">&nbsp;
                                                
											</TD>
											<TD align="left" width="123" bgColor="#edffeb">
												<TABLE id="Table14" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="61" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="45" bgColor="#edffeb"> </TD>
                                                
											<TD align="left" width="78" bgColor="#edffeb">
												<TABLE id="Table15" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
                                                            </TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table16" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table17" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                &nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="51" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="109" bgColor="#edffeb">
												<TABLE id="Table18" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" vAlign="bottom" align="left" width="50" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="121" bgColor="#edffeb">
												<TABLE id="Table19" cellSpacing="0" cellPadding="0" width="75" border="0">
													<TR>
														<TD class="textDadosCampo" width="75" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="69" bgColor="#edffeb"> </TD>
                                                &nbsp;
                                                
											<TD align="left" width="76" bgColor="#edffeb">
												<TABLE id="Table20" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table21" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table22" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR vAlign="middle">
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                &nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="95" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="100" bgColor="#edffeb">
												<TABLE id="Table23" cellSpacing="0" cellPadding="0" width="85" border="0">
													<TR>
														<TD class="textDadosCampo" width="85" bgColor="#edffeb">
														</TD>
													</TR>
												</TABLE>
											</TD>
											<TD class="textDadosOpcao" align="left" width="33" bgColor="#edffeb">
                                                
											</TD>
											<TD align="left" width="248" bgColor="#edffeb">
												<TABLE id="Table24" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosCampo" bgColor="#edffeb" width="247">
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
									<TABLE id="Table25" cellSpacing="0" cellPadding="0" width="490" border="0">
										<TR>
											<TD class="tableSpace"></TD>
										</TR>
									</TABLE>
									<TABLE id="Table26" cellSpacing="0" cellPadding="0" width="490" border="0" >
										<TR>
											<TD vAlign="middle" align="left" width="7" bgColor="#edffeb">&nbsp;</TD>
											<TD class="textDadosOpcao" vAlign="middle" align="right" width="7" bgColor="#edffeb">
                                                &nbsp;</TD>
											<TD class="textDadosOpcao" align="left" width="38" bgColor="#edffeb">
                                                </TD>
											<TD align="left" width="438" bgColor="#edffeb">
												<TABLE id="Table27" cellSpacing="0" cellPadding="0" border="0">
													<TR>
														<TD class="textDadosOpcao" width="437" bgColor="#edffeb">
                                                            </TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE> 
                                 
								</TD>  celula direita
								<TD class="normalFont" align="center" width="110">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <asp:image id="ImagemEmpregado" runat="server" Height="102px" Width="81px" 
                                        Visible="False"></asp:image><BR>
									<asp:button id="btnFichaCompleta" runat="server" CssClass="buttonFoto" Text="Cadastro Completo"
										Width="132px" Visible="False"></asp:button></TD>
                                        
                                            <caption>
                                                <input id="txtIdUsuario" type="text" visible="False"  style="visibility:hidden"  />
                                                <input id="txtIdEmpregado" type="text" visible="False" style="visibility:hidden" />
                                                <input id="txtIdEmpresa" type="text" visible="False" style="visibility:hidden"/>
                                </caption>
                                
							</TR>
						</TABLE>
					</TD> -->
				</TR>
				<TR>
					<TD noWrap align="center">
					    <br />
					</TD>
				</TR>
			</TABLE>

                    <div class="col-11 subtituloBG mt-4 mb-2" style="padding-top: 10px" >
                        <asp:label id="lblExCli" runat="server" SkinID="TitleFont" class="subtitulo">Exames Clínicos</asp:label>
           
                    </div>
            <div class="col-12 col-md-10 gx-3 gy-2 ps-1">

<TABLE class="defaultFont" cellSpacing="0" cellPadding="0" width="600" align="center" border="0">
				<TR>
					
				<TR>
					<TD vAlign="top" align="right">

                          
                            
              <eo:Grid ID="grd_Clinicos" runat="server" ColumnHeaderAscImage="00050403" 
                ColumnHeaderDescImage="00050404" FixedColumnCount="1" 
                GridLines="Both" Height="400px" Width="1050px"
                ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" 
                ItemHeight="30" KeyField="Id"  IsCallbackByMe="False"  
                            FullRowMode="false" EnableKeyboardNavigation="true"
          OnItemCommand="grd_Clinicos_ItemCommand" 
          ClientSideOnItemCommand="OnItemCommand" RunningMode="Server"  >

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
            <ColumnHeaderTextStyle CssText="text-align:center" />
            <ColumnHeaderStyle CssClass="tabelaC colunas" />
            <Columns>
                <eo:StaticColumn HeaderText="Data" AllowSort="True" 
                    DataField="Data" Name="Data" ReadOnly="True" 
                    SortOrder="Ascending" Text="" Width="120">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;"/>
                </eo:StaticColumn>


                <eo:CheckBoxColumn Name="chk1" Width="80" HeaderText="Imprimir"  ClientSideEndEdit="end_edit_back_ordered"  >
                </eo:CheckBoxColumn>

            <eo:CustomColumn HeaderText="Available On"  Width="0" 
              ClientSideGetText="get_avail_on_text"
              ClientSideBeginEdit="begin_edit_avail_on_date"
              ClientSideEndEdit ="end_edit_avail_on_date">                
            </eo:CustomColumn>


                <eo:StaticColumn HeaderText="Tipo" AllowSort="True" 
                    DataField="Tipo" Name="Tipo" ReadOnly="True" 
                    Width="300">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>
                <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                    DataField="Resultado" Name="Resultado" ReadOnly="True"  
                    Width="300">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;"/>
                </eo:StaticColumn>

                <eo:StaticColumn HeaderText="Id" AllowSort="False" 
                    DataField="Id" Name="Id" ReadOnly="True"  
                    Width="0">
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:StaticColumn>

                <eo:ButtonColumn ButtonText="Editar" 
                    Name="Selecionar" Width="0" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:ButtonColumn>

                <eo:ButtonColumn ButtonText="Visualizar" 
                    Name="Selecionar" Width="105" >
                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #B0ABAB !important; text-align: left; height: 30px !important;" />
                </eo:ButtonColumn>

  
            </Columns>
            <ColumnTemplates>
                <eo:TextBoxColumn>
                    <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma"/>
                </eo:TextBoxColumn>
                <eo:DateTimeColumn>
                    <datepicker controlskinid="None" daycellheight="16" daycellwidth="19" 
                        dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" 
                        selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" 
                        titlerightarrowimageurl="DefaultSubMenuIcon">
                        <PickerStyle 
                            CssText="border-bottom-color:#7f9db9;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#7f9db9;border-left-style:solid;border-left-width:1px;border-right-color:#7f9db9;border-right-style:solid;border-right-width:1px;border-top-color:#7f9db9;border-top-style:solid;border-top-width:1px;font-family:Courier New;font-size:8pt;margin-bottom:0px;margin-left:0px;margin-right:0px;margin-top:0px;padding-bottom:1px;padding-left:2px;padding-right:2px;padding-top:2px;" />
                        <CalendarStyle 
                            CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                        <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                        <TitleArrowStyle CssText="cursor:hand" />
                        <MonthStyle 
                            CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                        <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                        <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DayHoverStyle 
                            CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                        <TodayStyle 
                            CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                        <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <DisabledDayStyle 
                            CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                        <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                    </datepicker>
                </eo:DateTimeColumn>
                <eo:MaskedEditColumn>
                    <maskededit controlskinid="None" 
                        textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                    </maskededit>
                </eo:MaskedEditColumn>
            </ColumnTemplates>
            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
        </eo:Grid>

            
                    </TD>
				</TR>
				<TR>
					<TD class="normalFont" style="HEIGHT: 15px" align="center"><P>
							<TABLE class="defaultFont" id="Table28" cellSpacing="0" cellPadding="0" width="590" align="right"
								border="0">
								<TR>
									<TD align="right"><asp:label id="lblTotRegistros" runat="server"></asp:label></TD>
								</TR>
							</TABLE>
							<BR>
							</P>
					</TD>
				</TR>
			</TABLE>
            </div>
			<%--<iframe id="SubDados" name="SubDados" align="middle" marginWidth="0" marginHeight="0" frameBorder="0"
				width="600" scrolling="no" src="../DadosEmpresa/SubDadosNull.aspx" >
			</iframe>--%>
<%--		</form>
	</body>
</HTML>
--%>


                  <div class="col-12">
                      <div class="row">
                          <asp:Button ID="cmd_Gerar_PDF" onclick="cmd_Gerar_PDF_Click"  runat="server" 
                              CssClass="btn" Text="Gerar PDF" Width="132px" />
                          <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" 
                              CssClass="btn" Text="Voltar" Width="132px" />
                      </div>
                  </div>
                 <div class="col-12">
                      <div class="row">
                          <asp:TextBox ID="lblreg" runat="server" Width="0" ></asp:TextBox>
                          <asp:TextBox ID="lblnreg" runat="server" Width="0" ></asp:TextBox>
                      </div>
                  </div>
                  

        
           
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>



  
            </div>
    </div>
</asp:Content>