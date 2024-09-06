<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"    CodeBehind="CheckPCMSOPeriodos.aspx.cs" Inherits="Ilitera.Net.CheckPCMSOPeriodos" %>

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
           {
           }
    </style>

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >

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
                            
              

                return "N/A";
            }
            else {
                if (!cellValue) {

                  
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

    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:Label ID="lblTitulo" runat="server" CssClass="subtitulo">Documentos do PCMSO</asp:Label>
            </div>

            <div class="col-11">
                <div class="row">
                    <div class="col-md-4 gx-3 gy-2">
                        <asp:label id="lblPCMSO" runat="server" CssClass="tituloLabel">Selecione o Período</asp:label>
                        <asp:dropdownlist ID="ddlPCMSO2" runat="server" AutoPostBack="True" CssClass="texto form-select" OnSelectedIndexChanged="ddlPCMSO2_SelectedIndexChanged" ></asp:dropdownlist>
                    </div>

                    <div class="col-md-3 gx-3" style="margin-top: 28px;">
                        <asp:Button ID="btnPCMSO" runat="server" CssClass="btn" OnClick="lkbPCMSO_Click" Text="PCMSO - Documento Completo" />
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-md-4 gx-3 gy-2">
                        <div class="row">
                            <div class="col-12 gx-2 gy-2 ms-3">
                                <asp:CheckBox ID="chk_Medicos_Examinadores" runat="server" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" Text="Inserir Médicos Examinadores (com Exames)" OnCheckedChanged="chk_Medicos_Examinadores_CheckedChanged" />
                            </div>

                            <div class="col-12 gx-2 gy-2 ms-3">
                                <asp:CheckBox ID="chk_Medicos_Examinadores_Todos" runat="server" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" Text="Inserir Médicos Examinadores (Todos)" OnCheckedChanged="chk_Medicos_Examinadores_Todos_CheckedChanged"/>
                            </div>

                            <div class="col-12 gx-2 gy-2 ms-3">
                                <asp:CheckBox ID="chk_Inserir_Cronograma" runat="server" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" Text="Inserir Cronograma" />
                            </div>
                            <div class="col-12 gx-2 gy-2 mb-2 ms-3">
                                <asp:CheckBox ID="chk_Preposto" runat="server" AutoPostBack="True" CssClass="texto form-check-input bg-transparent border-0" Text="Inserir Preposto" OnCheckedChanged="chk_Preposto_CheckedChanged" />
                            </div>

                            <div class="col-12">
                                <asp:DropDownList ID="cmbPreposto" runat="server" Enabled="False" CssClass="texto form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                    <asp:Label ID="Label1" runat="server" CssClass="subtitulo" Text="Relatórios"></asp:Label>
                </div>

                <div class="col-12 mb-3">
                    <div class="row ps-2">
                        <div class="col-5">
                            <div class="row gx-3 gy-2 justify-content-center">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblExamesRealizados" runat="server" CssClass="tituloLabel me-2" Text="Exames Realizados por GHE"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnExamesRealizados" runat="server" Width="50px" CssClass="btnMenor2" Text="PDF" OnClick="lkbExamesRealizados_Click"  />
                                            <asp:Button ID="btnExamesRealizadosCSV" runat="server" Width="50px" CssClass="btnMenor2" Text="CSV" OnClick="lkbExamesRealizadosCSV_Click" />
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblExamesPlanejados" runat="server" CssClass="tituloLabel me-2" Text="Exames Planejados Ordenados por Data"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnExamesPlanejados" runat="server" Width="50px" CssClass="btnMenor2" Text="PDF" OnClick="lkbExamesPlanejados_Click" />
                                            <asp:Button ID="btnExamesPlanejadosCSV" runat="server" Width="50px" CssClass="btnMenor2" Text="CSV" OnClick="lkbExamesPlanejadosCSV_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblExamesPlanejadosnaoExecutados" runat="server" CssClass="tituloLabel me-2" Text="Exames Planejados e Não Executados"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnExamesPlanejadosnaoExecutados" runat="server" Width="50px" CssClass="btnMenor2" Text="PDF" OnClick="lkbExamesPlanejadosnaoExecutados_Click" />
                                            <asp:Button ID="btnExamesPlanejadosnaoExecutadosCSV" runat="server" Width="50px" CssClass="btnMenor2" Text="CSV" OnClick="lkbExamesPlanejadosnaoExecutadosCSV_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblPlanejEmpreg" runat="server" CssClass="tituloLabel me-2" Text="Planejamento de Exames por Empregado"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnPlanejEmpreg" runat="server" Width="50px" CssClass="btnMenor2" OnClick="lkbPlanejEmpreg_Click" Text="PDF" />
                                            <asp:Button ID="btnPlanejEmpregCSV" runat="server" Width="50px" CssClass="btnMenor2" Text="CSV" OnClick="lkbPlanejEmpregCSV_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblPlanejEx" runat="server" CssClass="tituloLabel me-2" Text="Planejamento de Exames por Tipo de Exame"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnPlanejEx" runat="server" Width="50px" CssClass="btnMenor2" Text="PDF" OnClick="lkbPlanejEx_Click" />
                                            <asp:Button ID="btnPlanejExCSV" runat="server" Width="50px" CssClass="btnMenor2" Text="CSV" OnClick="lkbPlanejExCSV_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-5">
                            <div class="row gx-3 gy-2 justify-content-center">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblPCMSO_Analitico" runat="server" CssClass="tituloLabel me-2" Text="PCMSO - Relatório Analítico"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnPCMSO_Analitico" runat="server" Width="50px" CssClass="btnMenor2" OnClick="lkbPCMSO_Analitico_Click" Text="PDF" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblNormasGerais" runat="server" CssClass="tituloLabel me-2" Text="PCMSO - Normas Gerais de Ação"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnNormasGerais" runat="server" Width="50px" CssClass="btnMenor2" OnClick="lkbNormasGerais_Click" Text="PDF" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblPlanilhaGlobal" runat="server" CssClass="tituloLabel me-2" Text="Planilha de Análise Global"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnPlanilhaGlobal" runat="server" Width="50px" CssClass="btnMenor2" OnClick="lkbPlanilhaGlobal_Click" Text="PDF" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-7">
                                            <asp:Label ID="lblRelatorioAnual" runat="server" CssClass="tituloLabel me-2" Text="Relatório Anual"></asp:Label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Button ID="btnRelatorioAnual" runat="server" Width="50px" CssClass="btnMenor2" OnClick="lkbRelatorioAnual_Click" Text="PDF"  />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
                    <%-- <div class="col-12 d-flex ms-2 me-4 justify-content-center" style="Width: 10px;">
                        <div class="vr"></div>
                    </div> --%>

            <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
                <asp:Label ID="lblPPRA0" runat="server" CssClass="subtitulo" Text="Certificado com Assinatura Digital"></asp:Label>
            </div>

            <div class="col-11 d-flex justify-content-center mb-3">
                <div class="row">
                    <div class="col-12">
                        <eo:Grid ID="grd_Clinicos" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" ColumnHeaderDescImage="00050404" 
                            ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" GridLines="Both" Height="227px" ItemHeight="30" KeyField="IdRepositorio" 
                            Width="640px" OnItemCommand="grd_Clinicos_ItemCommand"  >
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
                                <eo:StaticColumn AllowSort="True" DataField="DataHora" HeaderText="Data do Documento" Name="DataHora" ReadOnly="True" SortOrder="Ascending" Text="" Width="140" DataFormat="{0:dd/MM/yyyy}" DataType="DateTime">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Descrição" Name="Descricao" ReadOnly="True" Width="390">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:StaticColumn AllowSort="True" DataField="Anexo" HeaderText="Anexo" Name="Anexo" ReadOnly="True" Width="1">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:StaticColumn>
                                <eo:ButtonColumn ButtonText="Visualizar" Name="Selecionar" Width="90">
                                    <CellStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500; color: #7D7B7B !important; text-align: left; height: 35px !important; padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 35px !important;" />
                                </eo:ButtonColumn>
                            </Columns>
                            <columntemplates>
                                <eo:TextBoxColumn>
                                    <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
                                </eo:TextBoxColumn>
                                <eo:DateTimeColumn>
                                    <datepicker controlskinid="None" daycellheight="16" daycellwidth="19" dayheaderformat="FirstLetter" disableddates="" othermonthdayvisible="True" selecteddates="" titleleftarrowimageurl="DefaultSubMenuIconRTL" titlerightarrowimageurl="DefaultSubMenuIcon">
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
                                    </datepicker>
                                </eo:DateTimeColumn>
                                <eo:MaskedEditColumn>
                                    <maskededit controlskinid="None" textboxstyle-csstext="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; font-family:Courier New;font-size:8pt;">
                                    </maskededit>
                                </eo:MaskedEditColumn>
                            </columntemplates>
                            <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                        </eo:Grid>
                    </div>
                </div>
            </div>

            <asp:Button ID="btnPCMSOCSV" runat="server" CssClass="btnLogarClass" OnClick="lkbPCMSOCSV_Click" Width="290px" Text="CSV - PCMSO - Documento Completo" Enabled="False" Font-Size="X-Small" ForeColor="#999999" Visible="False" />
            <asp:ListBox ID="lst_Id_Preposto" runat="server" Height="20px" Visible="False" Width="88px"></asp:ListBox>
            <asp:Button ID="btnNormasGeraisCSV" runat="server" CssClass="btnLogarClass" OnClick="lkbNormasGeraisCSV_Click" Width="290px" Text="CSV - Normas Gerais de Ação" Enabled="False" Font-Size="X-Small" ForeColor="#999999" Visible="False" />
            <asp:Button ID="btnPlanilhaGlobalCSV" runat="server" CssClass="btnLogarClass" OnClick="lkbPlanilhaGlobalCSV_Click" Width="290px" Text="CSV - Planilha de Análise Global" Font-Size="X-Small" ForeColor="#999999" Visible="False" />
            <asp:Button ID="btnRelatorioAnualCSV" runat="server" CssClass="btnLogarClass" OnClick="lkbRelatorioAnualCSV_Click" Width="290px" Text="CSV - Relatório Anual" Enabled="False" Font-Size="X-Small" ForeColor="#999999" Visible="False" />


 <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
    </eo:MsgBox>

        </div>
    </div>
</asp:Content>
