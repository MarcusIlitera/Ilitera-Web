<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"
    CodeBehind="Repositorio.aspx.cs" Inherits="Ilitera.Net.RepositorioEPI" %>

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
            width: 503px;
        }
        .auto-style2 {
            width: 503px;
            height: 22px;
        }
        
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2 w-100">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="" Width="1080px">
	

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

        <%-- subtitulo --%>

        <div class="col-12 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="Label1" runat="server" SkinID="TitleFont" CssClass="subtitulo">Repositório EPI</asp:Label>
        </div>

        <%-- multipage --%>


							<eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1">
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Informações">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Ficha Digitalizada">
                                    </eo:TabItem>
                                </Items>
                            </topgroup>
                            <lookitems>
                                <eo:TabItem Height="21" ItemID="_Default" 
                                    NormalStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; background: #F1F1F1; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;"
                                    SelectedStyle-CssText="font-family:'Ubuntu'; font-size: 12px; color: #1C9489; font-weight: bold; padding: 10px 15px; background: #D9D9D9; border-radius: 8px; cursor: hand; width: fit-content; margin-right: 1rem;">
                                    <subgroup itemspacing="1" 
                                        style-csstext="font-family:'Ubuntu'; font-size: 12px; color: #B0ABAB; padding: 10px 10px; border-radius: 8px; cursor: hand; width: fit-content;">
                                    </subgroup> 
                                </eo:TabItem>
                            </lookitems>
                        </eo:TabStrip>

        

             <eo:MultiPage ID="MultiPage1" runat="server" Height="300" Width="1080">
            
                  <eo:PageView ID="Pageview1" runat="server" Width="1080px">

                      <div class="col-12">
                          <div class="flex-column">
                              <div class="col-md-2 gx-3 gy-2 mb-3 ms-3">
                                  <asp:Label ID="Label3" runat="server" CssClass="tituloLabel">Data da Ficha</asp:Label>
                                  <div class="row">
                                      <div class="col-9">
                                          <asp:TextBox ID="wdtDataExame" runat="server" CssClass="texto form-control" DisplayModeFormat="dd/MM/yyyy" HorizontalAlign="Center" ImageDirectory=" " MaxLength="10" Nullable="False"></asp:TextBox>
                                      </div>
                                  </div>
                              </div>

                              <div class="col-md-8 gx-3 gy-2 ms-3">
                                  <asp:Label ID="lblAnotacoes" runat="server" CssClass="tituloLabel">Descrição</asp:Label>
                                  <asp:TextBox ID="txtDescricao" runat="server" Height="146px" TextMode="MultiLine" CssClass="texto form-control"></asp:TextBox>
                              </div>
                          </div>
                      </div>


         </eo:PageView>

        <eo:PageView ID="Pageview2" runat="server" Width="1080px">

            <div class="container d-flex w-100">
                      <div class="row gx-2 gy-2 w-100">
                          <div class="col-md-5">
                              <asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Arquivo de Ficha</asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm"
                                   ReadOnly="True" Rows="4" TextMode="MultiLine" ></asp:TextBox>
                          </div>
                          <div class="col-md-6 ms-3" style="margin-left: 10px; margin-bottom: 30px;">
                              <asp:Label ID="Label6" runat="server" CssClass="tituloLabel form-label col-form-label col-form-label-sm">Inserir / Modificar Arquivo de Ficha</asp:Label>
                              <asp:Literal runat="server"><br /></asp:Literal>
                              <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" class="texto form-control form-control-sm" />   
                              <div class="row">
                                  <div class="mt-2">
                                      <asp:Button ID="cmd_PDF" runat="server" CssClass="btn" Text="Abrir PDF" Visible="False" OnClick="cmd_Abrir_PDF_Click" />
                                  </div>
                              </div>
                          </div>
                      </div>
                      <div class="row gx-2 gy-2">
                          <div class="col-12">
                              
                              <asp:Button ID="cmd_Imagem" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Visualizar Arquivo" Visible="False" OnClick="cmd_Imagem_Click" />
                              <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
                              <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="545px" Visible="False" Width="428px" />
                          </div>
                      </div>  
              </div>

       </eo:PageView>

       </eo:MultiPage>


        <%-- botões finais --%>

        <div class="col-11">
            <div class="row">

                <div class="text-center mb-3">
                    <asp:Button ID="btnOK" runat="server" tabIndex="1" Text="Gravar" CssClass="btn" onclick="btnOK_Click" />
                    <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn" onclick="btnExcluir_Click" />
                </div>

               <div class="text-start">
                    <asp:Button ID="cmd_Voltar" runat="server" CssClass="btn2" Text="Voltar" onclick="cmd_Voltar_Click" />
                </div>

              </div>
             </div>

            <input id="txtAuxiliar" runat="server" name="txtAuxiliar" type="hidden" />

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
