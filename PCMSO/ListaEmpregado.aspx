<%@ Page Language="C#"   AutoEventWireup="True"  MasterPageFile="~/Site1.Master" CodeBehind="ListaEmpregado.aspx.cs"  Inherits="Ilitera.Net.PCMSO.ListaEmpregado" Title="Ilitera.Net" %>

<%@ Register TagPrefix="uc1" TagName="Menu" Src="~/ucMenuLateral.ascx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

		<script language="javascript">
		    var tiporel;

		    function RelatorioN(mes) {
		        //if (document.FormEmpregado.rblTipoData_0.checked == true)                
                if ( document.aspnetForm.ctl00$ContentPlaceHolder1$TestePainel$tmpl1$rblTipoData_0.checked == true )
		            tiporel = "NA";
		        else
		            tiporel = "NN";

		        AbreRel(mes);
		    }

		    function RelatorioD(mes) {
		        //if (document.FormEmpregado.rblTipoData_0.checked == true)
		        if (document.aspnetForm.ctl00$ContentPlaceHolder1$TestePainel$tmpl1$rblTipoData_0.checked == true)
		            tiporel = "DA";
		        else
		            tiporel = "DN";

		        AbreRel(mes);
		    }

		    function AbreRel(mes) {
		        //document.FormEmpregado.txtTipoRel.value = tiporel;
		        document.aspnetForm.ctl00$ContentPlaceHolder1$TestePainel$tmpl1$txtTipoRel.value = tiporel;
		        //document.FormEmpregado.txtMesRel.value = mes;
		        document.aspnetForm.ctl00$ContentPlaceHolder1$TestePainel$tmpl1$txtMesRel.value = mes;

		        __doPostBack('ListaEmpregadoAgrupado', '');
		    }

		    function ListaDadosEmpregado(IdEmpregado, IdEmpresa, IdUsuario) {
		        top.window.document.getElementById('txtIdEmpregado').value = IdEmpregado;
		        window.location.href = "DadosEmpregado.aspx?IdEmpregado=" + IdEmpregado + "&IdEmpresa=" + IdEmpresa + "&IdUsuario=" + IdUsuario;
		    }

		</script>
    <ig:WebSplitter runat="server" ID="TestePainel" Height="700px">
        <Panes>
            <ig:SplitterPane Size="20%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>
                    <uc1:Menu runat="server" ID="Menu" />
                </Template>
            </ig:SplitterPane>
            <ig:SplitterPane Size="80%" Style="padding: 5px;" BackColor="#edffeb">
                <Template>

                    <igtxt:WebImageButton id="btnemp" runat="server" onclick="btnemp_Click" 
                        Text="Lista de Empregados" ></igtxt:WebImageButton>
                    &nbsp;&nbsp;<br />
                            <br />
                            

                        <span class="style1">
                    <asp:Label ID="lbl_Titulo" runat="server" Text="Exame - Lista de Empregados"></asp:Label>
                    <br />
                    <br />
                    </span>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Filtros : "></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rd_Todos" runat="server" Checked="True" GroupName="Func" 
                        Text="Todos" AutoPostBack="True" 
                        oncheckedchanged="rd_Todos_CheckedChanged" />
                    &nbsp;
                    <asp:RadioButton ID="rd_Ativos" runat="server" GroupName="Func" Text="Ativos" 
                        AutoPostBack="True" oncheckedchanged="rd_Ativos_CheckedChanged" />
                    &nbsp;
                    <asp:RadioButton ID="rd_Inativos" runat="server" GroupName="Func" 
                        Text="Inativos" AutoPostBack="True" 
                        oncheckedchanged="rd_Inativos_CheckedChanged" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Text="Nome Empregado : "></asp:Label>
                        &nbsp;<asp:TextBox ID="txt_Nome" runat="server" AutoPostBack="True" 
                        BackColor="#CCCCCC" CausesValidation="True" Height="18px" MaxLength="30" 
                        ontextchanged="txt_Nome_TextChanged" Width="83px"></asp:TextBox>
                        &nbsp;
                    <asp:Button ID="cmd_Busca" runat="server" Text="Busca" Font-Size="XX-Small" 
                        onclick="cmd_Busca_Click" />

                    <ig:WebDataGrid ID="gridEmpregados" runat="server" AutoGenerateColumns="False" 
                            DataKeyFields="IdEmpregado" Height="640px" 
                            OnCellSelectionChanged="gridEmpregados_CellSelectionChanged" Width="800px">
                            <Columns>
                                <ig:BoundDataField DataFieldName="NomeEmpregado" 
                                    Header-Text="Nome do Empregado" Key="NomeEmpregado">
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="DataAdmissao" Header-Text="Data de Admissão" 
                                    Key="DataAdmissao">
                                </ig:BoundDataField>
                            </Columns>
                            <editorproviders>
                                <ig:TextBoxProvider ID="TextBoxProvider">
                                </ig:TextBoxProvider>
                            </editorproviders>
                            <behaviors>
                                <ig:Paging Enabled="true" PagerAppearance="Both" 
                                    PagerMode="NextPreviousFirstLast" PageSize="25" >
                                </ig:Paging>
                                <ig:Selection CellSelectType="Single" Enabled="true">
                                    <SelectionClientEvents CellSelectionChanged="gridEmpregados_CellSelectionChanged" />
                                </ig:Selection>
                                <ig:RowSelectors Enabled="true" RowNumbering="true">
                                </ig:RowSelectors>
                            </behaviors>
                        </ig:WebDataGrid>

                    


                    

                    


                    

                    


                    

                    


</Template>
            </ig:SplitterPane>
        </Panes>
    </ig:WebSplitter>

    </asp:Content>