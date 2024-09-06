<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="Repositorio_Cadastro.aspx.cs" Inherits="Ilitera.Net.Repositorio_Cadastro" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="css/forms.css" rel="stylesheet" type="text/css" />
    <link href="css/tabelas.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

function on_column_gettext(column, item, cellValue)
{
    //if (cellValue == 1)
    //    return "Sim";
    //else
    //	return "Nao";
    return cellValue;
}

function on_begin_edit(cell)
{
    //Get the current cell value
    var v = 0;
    var valor = cell.getValue();
    
    if (valor == "Sim")
        v = 1;
    else
        v = 0;
    //alert(v);
    //Use index 0 if there is no value
    //if (v == null)
      //  v = 0;
    
    //Load the value into our drop down box
    var dropDownBox = document.getElementById("grade_dropdown");
    dropDownBox.selectedIndex = v;
}

function on_end_edit(cell)
{
    //Get the new value from the drop down box
    var dropDownBox = document.getElementById("grade_dropdown");
    var v = dropDownBox.selectedIndex;
    
    //Use null value if user has not selected a
    //value or selected "-Please Select-"
    //if (v == 0)    
      //  v = null;
    
    //Return the new value to the grid    
    //return v;

    if (v == 1)
        return "Sim";
    else
        return "Nao";
}


</script>

    <style type="text/css">
        .defaultFont
    {
        width: 586px;
            height: 20px;
        }
        
        .auto-style1 {
            width: 621px;
        }
        .auto-style2 {
            width: 621px;
            height: 22px;
        }
        
    .auto-style3 {
        font: xx-small Verdana;
        color: #44926D;
        width: 747px;
    }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="519px">
	

		<LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>
		<script language="javascript">
		function AbreClinicas(IdCliente, IdUsuario)
		{
			addItemPop(centerWin('../CommonPages/CadClinica.aspx?IdEmpresa=' + IdCliente + '&IdUsuario=' + IdUsuario + '', 360, 270, 'CadatroClinica'));
		}
		
		function AbreMedicos(IdUsuario)
		{
			var ddlClinicas = document.getElementById("ddlClinica");
			
			if (ddlClinicas.options[ddlClinicas.selectedIndex].value == "0")
				window.alert("É necessário selecionar uma Clínica para continuar!");
			else
				addItemPop(centerWin('../CommonPages/CadMedico.aspx?IdClinica=' + ddlClinicas.options[ddlClinicas.selectedIndex].value + '&IdUsuario=' + IdUsuario + '', 350, 310, 'CadastroMedico'));
		}
		</script>
<%--	</HEAD>
	<body>
		<form id="FormExameComplementar" method="post" runat="server">--%>

                            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Dados">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Arquivo Digitalizado">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <LookItems>
                                <eo:TabItem ItemID="_Default"
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                    <SubGroup OverlapDepth="8" ItemSpacing="5"
                                        Style-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px 10px 0px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </SubGroup>
                                </eo:TabItem>
                            </LookItems>
                        </eo:TabStrip>


             <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050"> <%-- PÁGINA 1: DADOS --%>
            
                  <eo:PageView ID="Pageview1" runat="server" Width="1050">

                     <div class="col-12 subtituloBG mb-3" style="padding-top: 10px;">
                          <asp:Label ID="Label1" runat="server" CssClass="subtitulo">Repositório Documentos/Laudos</asp:Label>
                      </div>

                      <div class="col-11 mt-4">
                        <div class="row">
                      <div class="col-md-5 gx-8 gy-4 m-3">
                                    <asp:RadioButtonList ID="rd_Documento" OnSelectedIndexChanged="rd_Documento_SelectedIndexChanged"  runat="server" CssClass="texto form-check-label bg-transparent border-0" CellPadding="0" CellSpacing="0" Font-Bold="False" RepeatColumns="5" Width="722px" Height="100px">
                                        <asp:ListItem Selected="True" Value="C">Caldeira</asp:ListItem>
                                        <asp:ListItem Value="V">Vaso de Pressão</asp:ListItem>
                                        <asp:ListItem Value="S">SPDA</asp:ListItem>
                                        <asp:ListItem Value="E">Laudo Elétrico</asp:ListItem>
                                        <asp:ListItem Value="P">Periculosidade</asp:ListItem>
                                        <asp:ListItem Value="T">Treinamento</asp:ListItem>                                        
                                        <asp:ListItem Value="A">PPRA</asp:ListItem>
                                        <asp:ListItem Value="M">PCMSO</asp:ListItem>
                                        <asp:ListItem Value="R">Relatório Gerencial</asp:ListItem>
                                        <asp:ListItem Value="O">Outros</asp:ListItem>                                        
                                        <asp:ListItem Value="L">LTCAT</asp:ListItem>
                                        <asp:ListItem Value="I">Insalubridade</asp:ListItem>
                                        <asp:ListItem Value="B">ASO</asp:ListItem>
                                        <asp:ListItem Value="1">Laudo Ergonômico</asp:ListItem>                                        
                                        <asp:ListItem Value="D">PPP</asp:ListItem>
                                        <asp:ListItem Value="X">PPR</asp:ListItem>
                                        <asp:ListItem Value="Z">PCA</asp:ListItem>
                                        <asp:ListItem Value="N">Relat.Anual</asp:ListItem>
                                        <asp:ListItem Value="Y">Banco de Dados</asp:ListItem>
                                        <asp:ListItem Value="H">Lista Campanhas</asp:ListItem>
                                        <asp:ListItem Value="G">Guia Encaminhamento</asp:ListItem>
                                        <asp:ListItem Value="J">COVID</asp:ListItem>                                        
                                        <asp:ListItem Value="W">PGR</asp:ListItem>
                                        <asp:ListItem Value="5">PGRTR</asp:ListItem>
                                    </asp:RadioButtonList></>
                        </div>
                       </div>
                          </div>




                      <div class="col-11">
                         <div class="row">
                      <div class="col-md-5 gx-3 gy-4 mb-8">
                           <asp:Label ID="Label2" runat="server" CssClass="tituloLabel form-label form-label-sm">Data Documento/Laudo</asp:Label>
                           <asp:TextBox ID="wdtDataExame" runat="server" CssClass="inputBox" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="10" Nullable="False" Width="100px" Font-Size="Small"></asp:TextBox>
                           </asp:TextBox>
                       </div>
                             </div>
                          </div>

                      <div class="col-11">
                         <div class="row">
                       <div class="col-md-5 gx-3 gy-4">  
                           <asp:Label ID="lblAnotacoes" runat="server" CssClass="tituloLabel form-label form-label-sm">Descrição do Documento/Laudo</asp:Label>
                           <asp:TextBox ID="txtDescricao" runat="server" CssClass="texto form-control form-control-sm" Height="210px" TextMode="MultiLine" Width="474px" MaxLength="500"></asp:TextBox>
                      </div>
                             </div>
                          </div>

         </eo:PageView>

        <eo:PageView ID="Pageview2" runat="server" Width="516px">    <%-- PÁGINA 2: ARQUIVO DIGITALIZADO --%>



                       <div class="col-11">
                         <div class="row">

                       <div class="col-12 gx-3 gy-2">
                            <asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel">Arquivo documento/laudo :</asp:Label>
                            <asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm" ReadOnly="True" Rows="4" TextMode="MultiLine">
                            </asp:TextBox>
                       </div>


                       <div class="col-md-9 gx-3 gy-2">
                           <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Inserir/Modificar Arquivo</asp:Label>
                           <asp:FileUpload ID="File1" runat="server" CssClass="texto form-control" ClientIDMode="Static"  />
                    </div>

                <%--    <div class="col-md-3 gx-3 gy-3 mt-4">
                        <asp:Button ID="cmd_Add" runat="server" Text="Adicionar Arquivo" CssClass="btn" style="margin-top: 4px" />
                    </div>
                      --%>
                      </div>
                       </div>
                                   
                                    <br />
                                    <br />
                                    <asp:Button ID="cmd_PDF" OnClick="cmd_PDF_Click"  runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Abrir Arquivo" Visible="False"/>
                                    <asp:Button ID="cmd_Imagem" OnClick="cmd_Imagem_Click"  runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Visualizar Arquivo" Visible="False" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <br />
                                    <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="406px" Visible="False" Width="351px" />                                    
            <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>

       </eo:PageView>

       </eo:MultiPage>

         <div class="col-12 mt-4">
          <div class="row text-center">
           <div>
             <asp:Button ID="btnOK" onclick="btnOK_Click"  runat="server" CssClass="btn" Text="Gravar"/>
             <asp:Button ID="btnExcluir" onclick="btnExcluir_Click"  runat="server" CssClass="btn" Text="Excluir"/>
          </div>
         </div>
          </div>
        
        <div class="col-11">
           <div class="row">
           <div class="col-md-5 gx-2 gy-4 mb-8">
               <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar"/>
          </div>    
            </div>
        </div>

            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden">

                <%--		</form>
	</body>
</HTML>
--%>
            </input>
                </eo:CallbackPanel>
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None" 
        HeaderHtml="Dialog Title" Height="100px" Width="168px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox> 

 </div>
   </div>
    </asp:Content>
