<%@ Page Title="Ilitera.Net" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True"   CodeBehind="ExameClinico.aspx.cs" Inherits="Ilitera.Net.ExameClinico" %>

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
    }
        .style7
        {
            width: 110px;
        }
        .style8
        {
            width: 17px;
        }
        .style9
        {
            width: 131px;
        }
        .style10
        {
            width: 119px;
        }
        .style11
        {
            width: 8px;
        }
        .inputBox
        {}
        .style15
        {
            width: 155px;
        }
        .style16
        {
            width: 359px;
        }
        .style17
        {
            width: 223px; 
        }
    .style18
    {
        width: 156px;
    }
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div class="container-fluid d-flex ms-5 ps-4">
        <div class="row gx-3 gy-2">

    <eo:CallbackPanel runat="server" id="CallbackPanel1" Triggers="">
	
        <LINK href="scripts/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="scripts/validador.js"></script>

        <%-- subtitulo --%>

        <div class="col-11 subtituloBG mb-3" style="padding-top: 10px;">
            <asp:Label ID="lblNome" runat="server" SkinID="TitleFont" CssClass="subtitulo">Exame Clínico</asp:Label>
        </div>

        <%-- primeira parte da página --%>

        <div class="col-11 mb-3">
            <div class="row">

                <div class="col-md-3 gx-3 gy-2">
                    <asp:Label ID="Label27" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Resultado do Exame</asp:Label>
                    <div class="row">
                        <div class="col-12 gx-8 gy-1 ms-3">
                            <asp:RadioButtonList ID="rblResultado" runat="server" RepeatColumns="3" tabIndex="1" CssClass="texto form-control-sm" Width="250"> 
                                <asp:ListItem Value="1">Apto</asp:ListItem>
                                <asp:ListItem Value="2">Inapto</asp:ListItem>
                                <asp:ListItem Value="3">Em Espera</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>

                <div class="col-md-2 gx-3 gy-2">
                    <asp:Label ID="lblData" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Data do Exame</asp:Label>
                    <asp:TextBox ID="wdtDataExame" runat="server" MaxLength="10" CssClass="texto form-control form-control-sm"></asp:TextBox>
                </div>

                <div class="col-md-3 gx-3 gy-2">
                    <asp:Label ID="lblTipo" runat="server" SkinID="BoldFont" CssClass="tituloLabel form-label">Tipo do Exame</asp:Label>
                    <asp:DropDownList ID="ddlTipoExame" onselectedindexchanged="ddlTipoExame_SelectedIndexChanged" runat="server" tabIndex="1" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                </div>

                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="lblClinica" runat="server" CssClass="tituloLabel form-label">Clínica</asp:Label>
                    <asp:DropDownList ID="ddlClinica" onselectedindexchanged="ddlClinica_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                </div>

                <div class="col-md-4 gx-3 gy-2">
                    <asp:Label ID="lblMedico" runat="server" CssClass="tituloLabel form-label">Médico</asp:Label>
                    <asp:DropDownList ID="ddlMedico" runat="server" CssClass="texto form-select form-select-sm"></asp:DropDownList>
                </div>

                <div class="col-md-4 gy-2 ms-4" style="margin-top: 33px">
                    <asp:CheckBox ID="chk_TodosMedicos" OnCheckedChanged="chk_TodosMedicos_CheckedChanged" runat="server" AutoPostBack="True" Checked="True" CssClass="texto form-check-input bg-transparent border-0" Text="( Todos Médicos )" />
                </div>

                <asp:Label ID="lbl_Demissao2" runat="server" Font-Size="X-Small" ForeColor="#CC0000" Text="Data Demissão:" Visible="False"></asp:Label>
                <asp:Label ID="lblDemissao" runat="server" Font-Size="X-Small" ForeColor="#CC0000" Visible="False"></asp:Label>
                <asp:CheckBox ID="chk_PacienteCritico"  OnCheckedChanged="Chk_PacienteCritico_CheckedChanged" runat="server" AutoPostBack="True" CssClass="boldFont" Text="Paciente Crítico" Visible="False" />
            </div>
        </div>

        <%-- multipage --%>

        <div class="col-11">
            <eo:TabStrip ID="TabStrip1" runat="server" ControlSkinID="None" 
                            MultiPageID="MultiPage1" >
                            <topgroup>
                                <Items>
                                    <eo:TabItem Text-Html="Anamnese">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Exame Físico">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Indicadores Morbidade">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anotações / Obs.">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Exames Complementares">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Digitalização">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Anamnese Dinâmica">
                                    </eo:TabItem>
                                    <eo:TabItem Text-Html="Antecedentes">
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

            <eo:MultiPage ID="MultiPage1" runat="server" Height="400" Width="1050" CssClass="mb-3">
                                <eo:PageView ID="Pageview1" runat="server">
                                    <table ID="Table5" align="left" border="0" cellpadding="0" cellspacing="0"
                                        class="defaultFont mb-3" width="1050">
                                                
                                        <%-- PÁGINA 1: ANAMNESE --%>

                                    <div class="col-12">
                                        <div class="row">

                                       <div class="col-6">
                                          <div class="row">
                                                    <div class="col-6 gx-4 gy-3">
                                                          <asp:Label ID="lblQueixas" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Você está bem de saúde?</asp:Label>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                          <asp:CheckBox ID="ckbQueixaS" runat="server"  CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                          <asp:CheckBox ID="ckbQueixaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                    </div>
                                                         
                                          </div>
                                            </div>     
                                                   
                                        <div class="col-6">
                                          <div class="row">
                                                     <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblAfastado" runat="server" CssClass="texto form-check-label bg-transparent border-0"  SkinID="BoldFont">- Fica gripado com frequência?</asp:Label>
                                                     </div>
                                                     <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbGripadoS" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Sim"/>
                                                     </div>
                                                     <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbGripadoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>                                                            
                                                     </div>
                                        </div>
                                           </div>
                                          
                                                   
                                        <div class="col-6">
                                          <div class="row">     
                                                        <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblBronquite" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Bronquite, asma, rinite?</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbBronquiteS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbBronquiteN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                      </div>
                                        </div>
                                                       
                                        <div class="col-6">
                                          <div class="row"> 
                                                        <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label3" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Escuta bem?</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEscutaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEscutaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                         </div>
                                         </div>
                                           </div>     

                                        <div class="col-6">
                                          <div class="row">
                                                    <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblDoenca" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Doenças Digestivas?</asp:Label>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDigestivaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                    </div>
                                                   <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDigestivaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                   </div>
                                         </div>
                                           </div>

                                        <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3">
                                                         <asp:Label ID="lblMedicamentos" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Tem dores nas costas ou coluna?</asp:Label>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoresCostaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoresCostaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                 </div>
                                         </div>
                                           </div>            

                                        <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblCirurgia" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Doenças do estômago?</asp:Label>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEstomagoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEstomagoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                 </div>
                                        </div>
                                           </div>

                                        <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblTabagista" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Tem reumatismo?</asp:Label>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbReumatismoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbReumatismoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                  </div>
                                          </div>
                                            </div>

                                         <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblTrauma" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Enxerga bem?</asp:Label>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEnxergaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEnxergaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                 </div>
                                           </div>
                                             </div>

                                        <div class="col-6">
                                          <div class="row">
                                                  <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblEtilista" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Já esteve hospitalizado alguma vez?</asp:Label>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbCirurgiaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbCirurgiaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                  </div>           
                                            </div>
                                             </div>

                                        <div class="col-6">
                                          <div class="row">
                                                  <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblDeficiencia" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Tem dor de cabeça?</asp:Label>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDorCabecaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDorCabecaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                  </div>
                                            </div>
                                             </div>
                                                   
                                        <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="lblEtilista0" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Tem alergia?</asp:Label>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAlergiaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAlergiaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                 </div>
                                            </div>
                                             </div>

                                         <div class="col-6">
                                          <div class="row">
                                                  <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label4" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Já desmaiou alguma vez?</asp:Label>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDesmaioS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                  </div>
                                                  <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDesmaioN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                  </div>
                                            </div>
                                              </div>
                                               
                                         <div class="col-6">
                                          <div class="row">
                                                 <div class="col-6 gx-4 gy-3 mt-2">
                                                            <asp:Label ID="Label5" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Fuma?</asp:Label>
                                                </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbTabagismoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                 </div>
                                                <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbTabagismoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                </div>
                                  </div>
                                    </div>

                                        <div class="col-6">
                                          <div class="row">
                                                    <div class="col-6 gx-4 gy-3">                                                        
                                                            <asp:Label ID="Label8" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Já teve alguma fratura?</asp:Label>
                                                        </div>
                                                     <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbTraumatismoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                     </div>
                                                     <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbTraumatismoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                    </div>
                                             </div>
                                               </div>

                                        <div class="col-6">
                                          <div class="row">
                                                   <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label10" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Bebe?</asp:Label>
                                                        </div>
                                                   <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAlcoolismoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                       </div>
                                                   <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAlcoolismoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                            </div>
                                               </div>

                                        <div class="col-6">
                                          <div class="row">
                                                   <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label12" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Já ficou afastado?</asp:Label>
                                                        </div>
                                                   <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAfastamentoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                       </div>
                                                   <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAfastamentoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                            </div>
                                               </div>

                                        <div class="col-6">
                                          <div class="row">
                                                     <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label13" runat="server" CssClass="texto form-check-label bg-transparent border-0"  SkinID="BoldFont">- Pratica esporte?</asp:Label>
                                                        </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEsporteS" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Sim"/>
                                                        </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbEsporteN" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Não"/>
                                                        </div>
                                              </div>
                                                 </div>   

                                        <div class="col-6">
                                          <div class="row">
                                                    <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label14" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Doenças do coração?</asp:Label>
                                                        </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoencaCoracaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Sim"/>
                                                        </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoencaCoracaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Não"/>
                                                        </div>
                                             </div>
                                                 </div>

                                        <div class="col-6">
                                          <div class="row">
                                                      <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label15" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Faz uso de medicamento contínuo?</asp:Label>
                                                        </div>
                                                      <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbMedicacoesS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                      <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbMedicacoesN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                              </div>
                                                 </div>

                                        <div class="col-6">
                                          <div class="row">
                                                      <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label16" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Doenças urinárias?</asp:Label>
                                                        </div>
                                                      <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbUrinariaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                      <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbUrinariaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                               </div>
                                                 </div>

                                         <div class="col-6">
                                          <div class="row">
                                                        <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label17" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Alguma doença não mencionada?</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoencaCronicaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDoencaCronicaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                               </div>
                                                 </div>

                                        <div class="col-6">
                                          <div class="row">
                                                        <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label18" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Diabetes?</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDiabetesS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                         </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbDiabetesN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                              </div>
                                                 </div>

                                        <div class="col-6">
                                          <div class="row">
                                                        <div class="col-6 gx-4 gy-3">
                                                            <asp:Label ID="Label19" runat="server" CssClass="texto form-check-label bg-transparent border-0"  SkinID="BoldFont">- Já se acidentou no trabalho?</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAcidentouS" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAcidentouN" runat="server" CssClass="texto form-check-label bg-transparent border-0"  Text="Não"/>
                                                        </div>
                                               </div>
                                            </div>

                                            <div class="col-6">
                                                <div class="row">
                                                    <div class="col-6 gx-4 gy-3">
                                                        <asp:Label ID="Label50" runat="server" SkinID="BoldFont" CssClass="texto form-check-label bg-transparent border-0">- Colesterol alterado?</asp:Label>
                                                    </div>

                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="ckbColesterolS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="ckbColesterolN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-6">
                                                <div class="row">
                                                    <div class="col-6 gx-4 gy-3">
                                                        <asp:Label ID="Label52" runat="server" SkinID="BoldFont" CssClass="texto form-check-label bg-transparent border-0">- Hipertensão Arterial?</asp:Label>
                                                    </div>

                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="ckbHipertensaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="ckbHipertensaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         </div>

                                          <div class="col-12">
                                             <div class="row">
                                                        <div class="col-6 gx-4 gy-4">
                                                     <asp:Label ID="Label30" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">ANTECEDENTES FAMILIARES</asp:Label>
                                                </div>
                                          </div>
                                              </div>

                                           <div class="col-12">
                                             <div class="row">
                                                      <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label20" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Hipertensão Arterial</asp:Label>
                                                        </div>
                                                       <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFHipertensaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                      <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFHipertensaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                            </div>
                                              </div>

                                            <div class="col-12">
                                             <div class="row">
                                                        <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="lblObesidade" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont" >-Obesidade</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFObesidadeS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFObesidadeN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>                                                            
                                                        </div>
                                              </div>
                                                 </div>

                                            <div class="col-12">
                                             <div class="row">
                                                        <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label23" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Diabetes</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFDiabetesS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFDiabetesN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                              </div>
                                                 </div>
                                                     
                                             <div class="col-12">
                                              <div class="row">
                                                         <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label24" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Câncer</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFCancerS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFCancerN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>                                                            
                                                        </div>
                                              </div>
                                                 </div>

                                            <div class="col-12">
                                              <div class="row">
                                                        <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label25" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Doenças do coração</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFCoracaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFCoracaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>

                                               </div>
                                                 </div>

                                             <div class="col-12">
                                              <div class="row">
                                                         <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label26" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Colesterol alto</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFColesterolS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFColesterolN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>                                                            
                                                        </div>
                                             </div>
                                                </div>

                                            <div class="col-12">
                                              <div class="row">
                                                        <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label28" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Derrames cerebrais</asp:Label>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFDerramesS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                        <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFDerramesN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                        </div>
                                             </div>
                                                </div>

                                            <div class="col-12">
                                              <div class="row">
                                                       <div class="col-6 gx-4 gy-4">
                                                            <asp:Label ID="Label29" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">-Trat.Psiquiátricos</asp:Label>
                                                        </div>
                                                       <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFPsiquiatricosS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                        </div>
                                                       <div class="col-1 gx-2 gy-1 mt-3">
                                                            <asp:CheckBox ID="ckbAFPsiquiatricosN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>                                                            
                                                        </div>
                                                </div>
                                                 </div>

                                        <div class="col-12">
                                                  <div class="row">
                                                      <div class="col-6 gx-4 gy-4">
                                                          <asp:Label ID="Label38" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">AVALIAÇÃO OTOLÓGICA</asp:Label>
                                                      </div>
                                                  </div>
                                              </div>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-6 gx-4 gy-4">
                                                        <asp:Label ID="Label47" runat="server" SkinID="BoldFont" CssClass="texto">O canal auditivo ou membrana timpânica apresentam alguma obstrução?</asp:Label>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_ObstrucaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_ObstrucaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-6 gx-4 gy-4">
                                                        <asp:Label ID="Label48" runat="server" CssClass="texto" SkinID="BoldFont">O canal auditivo apresenta acúmulo de cerúmen ?</asp:Label>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_CerumenS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_CerumenN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>  
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-6 gx-4 gy-4">
                                                        <asp:Label ID="Label49" runat="server" SkinID="BoldFont" CssClass="texto">O canal auditivo apresenta alguma alteração ?</asp:Label>
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_AlteracaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                                    </div>
                                                    <div class="col-1 gx-2 gy-1 mt-3">
                                                        <asp:CheckBox ID="chk_Has_Otologica_AlteracaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não" />
                                                    </div>
                                                </div>
                                            </div>
                                            </table>
                                                
                                              
                                </eo:PageView>

                                <eo:PageView ID="Pageview2" runat="server" Width="1050px">

                                             <%-- PÁGINA 2: EXAME FÍSICO --%>

                                            <div class="col-12">  
                                             <div class="row">

                                            <div class="col-6">
                                                  <div class="row">
                                                    <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="Label7" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">Altura:</asp:Label>
                                                         <%--<igtxt:WebNumericEdit id="wneAltura" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Float" AutoPostBack="true" OnValueChange="wneAltura_ValueChange"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wneAltura" runat="server" CssClass="texto form-control form-control-sm" width="90px" AutoPostBack="True"></asp:TextBox>
                                                     </div>
                                                    </div>
                                                </div>
                             
                                                <div class="col-6">
                                                  <div class="row">
                                                <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="Label9" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">Peso(Kg):</asp:Label>
                                                        <%--<igtxt:WebNumericEdit id="wnePeso" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Float" AutoPostBack="true" OnValueChange="wnePeso_ValueChange"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wnePeso" runat="server" CssClass="texto form-control form-control-sm" width="90px" AutoPostBack="True"></asp:TextBox>
                                                </div>
                                                  </div>
                                                </div>
                                                        
                                                <div class="col-6">
                                                  <div class="row">
                                                <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="DUM" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">D.U.M.:</asp:Label>
                                                        <%--<igtxt:WebDateTimeEdit id="wdtDUM" runat="server" HorizontalAlign="Left" DisplayModeFormat="dd/MM/yyyy" ImageDirectory=" " Width="90px"></igtxt:WebDateTimeEdit></TD>--%>
                                                            <asp:TextBox ID="wdtDUM" runat="server" CssClass="texto form-control form-control-sm" width="90px"></asp:TextBox>
                                               </div>
                                                  </div>
                                              </div>
                                                            
                                               <div class="col-6">
                                                  <div class="row">
                                                <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="Label40" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">PA(mmHg):</asp:Label>
                                                            <asp:TextBox ID="txtPA" runat="server" CssClass="texto form-control form-control-sm" Width="90px"></asp:TextBox>
                                              </div>
                                                  </div>
                                              </div>

                                                 <div class="col-6">
                                                  <div class="row">
                                                <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="Label21" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">Pulso:</asp:Label>
                                                             <%--<igtxt:WebNumericEdit id="wnePulso" runat="server" Nullable="False" ImageDirectory=" " Width="90px" DataMode="Int"></igtxt:WebNumericEdit></TD>--%>
                                                            <asp:TextBox ID="wnePulso" runat="server" CssClass="texto form-control form-control-sm" width="90px"></asp:TextBox>
                                              </div>
                                                  </div>
                                              </div>

                                                 <div class="col-6">
                                                  <div class="row">
                                                <div class="col-2 gx-4 gy-2">
                                                            <asp:Label ID="Label3325" runat="server" CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">IMC:</asp:Label>
                                                            <asp:Label ID="lblIMC" runat="server"  CssClass="texto form-control form-control-sm" width="90px"></asp:Label>
                                              </div>
                                                  </div>
                                              </div>
                                               
                                            <div class="col-12">
                                                  <div class="row">

                                                  <div class="col-12">
                                                   <div class="row">
                                                <div class="col-6 gx-4 gy-4">
                                                  <asp:Label ID="Label39" runat="server"  CssClass="tituloLabel form-label form-label-sm" SkinID="BoldFont">Alteração</asp:Label>
                                             </div>
                                                </div>
                                             </div>
                                        </div>
                                    </div>
                                                

                                                 <div class="col-12">
                                                      <div class="row">
                                                 <div class="col-6 gx-4 gy-4">
                                                     <asp:Label ID="Label" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Pele e anexos</asp:Label>
                                                 </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                     <asp:CheckBox ID="ckbPeleS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                                </div>
                                                 <div class="col-1 gx-2 gy-1 mt-3">
                                                     <asp:CheckBox ID="ckbPeleN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                                 </div>
                                                </div>
                                             </div>
                                                        
                                                        
                                            <div class="col-12">
                                                 <div class="row">

                                            <div class="col-6 gx-4 gy-4">
                                                 <asp:Label ID="Label34" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Pulmões</asp:Label>
                                            </div>
                                            <div class="col-1 gx-2 gy-1 mt-3">
                                                 <asp:CheckBox ID="ckbPulmoesS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                            </div>
                                            <div class="col-1 gx-2 gy-1 mt-3">
                                                 <asp:CheckBox ID="ckbPulmoesN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                            </div>
                                        </div>
                                    </div>

                                                       
                                            <div class="col-12">
                                              <div class="row">
                                            <div class="col-6 gx-4 gy-4">
                                                 <asp:Label ID="Label31" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Osteo muscular</asp:Label>
                                            </div>
                                            <div class="col-1 gx-2 gy-1 mt-3">
                                                <asp:CheckBox ID="ckbOsteoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim" />
                                             </div>
                                            <div class="col-1 gx-2 gy-1 mt-3">
                                                <asp:CheckBox ID="ckbOsteoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não" />
                                          </div>
                                        </div>
                                        </div>         
                                                 
                                            <div class="col-12">
                                              <div class="row">
                                            <div class="col-6 gx-4 gy-4">
                                                 <asp:Label ID="Label35" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Abdomem</asp:Label>
                                           </div>
                                           <div class="col-1 gx-2 gy-1 mt-3">
                                                <asp:CheckBox ID="ckbAbdomemS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                            </div>
                                           <div class="col-1 gx-2 gy-1 mt-3">
                                                <asp:CheckBox ID="ckbAbdomemN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                          </div>
                                      </div>
                                    </div>
                                                  
                                           <div class="col-12">
                                              <div class="row">
                                            <div class="col-6 gx-4 gy-4">
                                                 <asp:Label ID="Label32" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Cabeça e pescoço</asp:Label>
                                            </div>
                                         <div class="col-1 gx-2 gy-1 mt-3">
                                              <asp:CheckBox ID="ckbCabecaS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                        </div>
                                         <div class="col-1 gx-2 gy-1 mt-3">
                                              <asp:CheckBox ID="ckbCabecaN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                         </div>
                                      </div>
                                    </div>
                                
                                 <div class="col-12">
                                     <div class="row">
                                   <div class="col-6 gx-4 gy-4">
                                        <asp:Label ID="Label36" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Membros superiores</asp:Label>
                                   </div>
                                  <div class="col-1 gx-2 gy-1 mt-3">
                                       <asp:CheckBox ID="ckbMSS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                                 </div>
                                  <div class="col-1 gx-2 gy-1 mt-3">
                                       <asp:CheckBox ID="ckbMSN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                                </div>
                            </div>
                         </div>
                                                   
                            <div class="col-12">
                                <div class="row">
                              <div class="col-6 gx-4 gy-4">
                                   <asp:Label ID="Label33" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Coração</asp:Label>
                            </div>
                          <div class="col-1 gx-2 gy-1 mt-3">
                               <asp:CheckBox ID="ckbCoracaoS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                        </div>
                         <div class="col-1 gx-2 gy-1 mt-3">
                              <asp:CheckBox ID="ckbCoracaoN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                       </div>
                    </div>
                </div>
                     
                        <div class="col-12">
                             <div class="row">
                          <div class="col-6 gx-4 gy-4">
                               <asp:Label ID="Label37" runat="server" CssClass="texto form-check-label bg-transparent border-0" SkinID="BoldFont">- Membros inferiores</asp:Label>
                          </div>
                           <div class="col-1 gx-2 gy-1 mt-3">
                                <asp:CheckBox ID="ckbMIS" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Sim"/>
                          </div>
                        <div class="col-1 gx-2 gy-1 mt-3">
                                <asp:CheckBox ID="ckbMIN" runat="server" CssClass="texto form-check-label bg-transparent border-0" Text="Não"/>
                        </div>
                    </div>
                 </div>   
        </div>
      </div>
                                               
                                </eo:PageView>

                                <eo:PageView ID="Pageview3" runat="server" width="1050">
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" 
                                        class="defaultFont" width="1050">
                                        <tr>

                                   <%--PÁGINA 3: INDICADORES DE MORBIDADE --%>

                <div class="col-12">
	              <div class="row">
                   <div class="col-md-5 gx-3 gy-2">
                     <asp:Label ID="Label1" runat="server" CssClass="tituloLabel form-label" SkinID="BoldFont" Text="Fatores de Risco"></asp:Label>
                      <asp:ListBox ID="lsbFatorRisco" runat="server" CssClass="texto form-control" Rows="9" CausesValidation="True"></asp:ListBox>
                </div>

			        <div class="col-2 gx-5 gy-2 text-center mt-3">
                      <div class="row">
                       <div class="col-12 gy-1">
                             <asp:imagebutton id="imbAddAll" OnClick="imbAddAll_Click"  runat="server" Enabled="False" CssClass="btnMenor" Width="40px" Text=">" ToolTip="Adiciona todos" ImageUrl="Images/right.svg" Style="padding: .4rem;">
                              </asp:imagebutton>
                        </div>
                                                
			           <div class="col-12 gy-1">
                            <asp:imagebutton id="imbAdd" OnClick="imbAdd_Click"  runat="server" Enabled="False" CssClass="btnMenor" Width="40px" Text=">" ToolTip="Adicionar selecionados" ImageUrl="Images/right.svg" Style="padding: .4rem;">
                            </asp:imagebutton>
                        </div>
                                               
			           <div class="col-12 gy-1">
                            <asp:imagebutton id="imbRemove" OnClick="imbRemove_Click"  runat="server" Enabled="False" CssClass="btnMenor" Width="40px" Text="<" ToolTip="Remover selecionados" ImageUrl="Images/left.svg" Style="padding: .4rem;">
                            </asp:imagebutton>
                       </div>
                                          
			          <div class="col-12 gy-1">
                           <asp:imagebutton id="imbRemoveAll" OnClick="imbRemoveAll_Click"  runat="server" Enabled="False"  CssClass="btnMenor" Width="40px" Text="<<" ToolTip="Remover todos" ImageUrl="Images/double-left.svg" Style="padding: .4rem;"></asp:imagebutton>
                      </div>
                       
                  </div>
                   </div>

	              <div class="col-md-5 pt-2 gx-3 gy-2">
                      <asp:Label ID="Label2" runat="server" CssClass="tituloLabel form-label" SkinID="BoldFont" Text="Fatores de Risco selecionados"></asp:Label>
                       <asp:ListBox ID="lsbFatorRiscoSel" runat="server" CssClass="texto form-control" Rows="9" SelectionMode="Multiple" SkinID="YellowList"></asp:ListBox>
                </div>

                </div>
              </div>
 
       <div class="col-2 gx-5 gy-2 text-center mt-3">
                           <div class="row">
			                  <div class="col-12 gy-1">
                                  <asp:Image ID="Image1" runat="server" Visible="False" ImageUrl="img/5pixel.gif" />
                               </div>
                          </div>
                        </div>

            <div class="col-12 mb-3">
            <div class="row text-center">
                <div>
                    <asp:Button ID="btnNovoFatorRisco" onclick="btnNovoFatorRisco_Click" runat="server" CssClass="btn" Text="Novo Fator de Risco" Width="135px"/>
                </div>
            </div>
        </div>

                            
                          
                                                                  <td align="right">
                                                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                                                                        <asp:Label ID="lblTotFatRisco" runat="server"></asp:Label>
                                                                  
                 
                                                        </td>
                                                        <td align="left" valign="top" width="260">
                                                           
                                                            <asp:Label ID="lbl_Atualiza" runat="server" Text="0" Visible="False"></asp:Label>
                                                            <br />
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" 
                                                                class="defaultFont" width="240">
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="lblTotFatRiscoSel" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                </eo:PageView>

                                <eo:PageView ID="Pageview4" runat="server" Width="1050px">

                                    <div class="col-11">
                                        <div class="row">

                                            <div class="col-12 gx-3 gy-2 mb-3">
                                                <asp:Label runat="server" CssClass="tituloLabel form-label">Anotações - Impresso na Ficha Clínica</asp:Label>
                                                <asp:TextBox ID="txtAnotacoes" runat="server" Height="102px" Rows="11" tabIndex="1" TextMode="MultiLine" MaxLength="1000" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-12 gx-3 gy-2 text-start mb-3">
                                                <asp:Button ID="btnResetText" OnClick="btnResetText_Click"  runat="server" Text="Atualizar anotações" CssClass="btn" />
                                            </div>

                                            <div class="col-12 gx-3 gy-2 mb-3">
                                                <asp:Label runat="server" CssClass="tituloLabel form-label">Observações - Apenas visualização</asp:Label>
                                                <asp:TextBox ID="txtObs" runat="server" Height="95px" Rows="11" tabIndex="1" TextMode="MultiLine" MaxLength="255" CssClass="texto form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T1" ID="chk_apt_Altura" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T1" ID="chk_inapt_Altura" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-3 ms-4 gy-2">
                                                        <asp:Label ID="lbl_apt_Altura" runat="server" Text="Aptidão para trabalho em altura" enabled="False" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T2" ID="chk_apt_Confinado" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T2" ID="chk_inapt_Confinado" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-4 ms-4 gy-2">
                                                        <asp:Label  ID="lbl_apt_Confinado" runat="server" Text="Aptidão para trabalho em espaços confinados" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T3" ID="chk_apt_Transportes" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T3" ID="chk_inapt_Transportes" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label  ID="lbl_apt_Transportes" runat="server" AutoPostBack="True" Text="Aptidão para operar equipamentos de transporte motorizados" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T4" ID="chk_apt_Submerso" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T4" ID="chk_inapt_Submerso" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label  ID="lbl_apt_Submerso" runat="server" AutoPostBack="True" Text="Aptidão para atividades submersas" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T5" ID="chk_apt_Eletricidade" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T5" ID="chk_inapt_Eletricidade" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_apt_Eletricidade" runat="server" AutoPostBack="True" Text="Aptidão para serviço em eletricidade" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T6" ID="chk_apt_Aquaviarios"  runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T6" ID="chk_inapt_Aquaviarios" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_apt_Aquaviarios" runat="server" AutoPostBack="True" Text="Aptidão para serviços aquaviários" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T7" ID="chk_apt_Alimento"  runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T7" ID="chk_inapt_Alimento" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_Apt_Alimento" runat="server" AutoPostBack="True" Text="Aptidão para manipular alimentos" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T8" ID="chk_Apt_Brigadista"  runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T8" ID="chk_Inapt_Brigadista" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_Apt_Brigadista" runat="server" AutoPostBack="True" Text="Aptidão para Brigadista" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton GroupName="T9" ID="chk_Apt_Socorrista"  runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton GroupName="T9" ID="chk_Inapt_Socorrista" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_Apt_Socorrista" runat="server" AutoPostBack="True" Text="Aptidão para Socorrista" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton ID="chk_Apt_Respirador" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton ID="chk_Inapt_Respirador" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_Apt_Respirador" runat="server" AutoPostBack="True" Text="Aptidão para uso de Respirador" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-md-1 ms-5 gy-2">
                                                        <asp:RadioButton ID="chk_Apt_Radiacao" runat="server" Text="Apto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-1 ms-4 gy-2">
                                                        <asp:RadioButton ID="chk_Inapt_Radiacao" runat="server" Text="Inapto" enabled="false" CssClass="texto form-check-input bg-transparent border-0" />
                                                    </div>

                                                    <div class="col-md-5 ms-4 gy-2">
                                                        <asp:Label ID="lbl_Apt_Radiacao" runat="server" AutoPostBack="True" Text="Aptidão para Radiação Ionizante" enabled="false" CssClass="texto" />
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </eo:PageView>

                                <eo:PageView ID="Pageview5" runat="server" Width="1050">


                                <eo:Grid ID="grd_Complementares" runat="server" ClientSideOnItemCommand="OnItemCommand" ColumnHeaderAscImage="00050403" 
                                    ColumnHeaderDescImage="00050404" ColumnHeaderDividerOffset="6" ColumnHeaderHeight="30" FixedColumnCount="1" GridLines="Both" 
                                    Height="260px" ItemHeight="30" KeyField="Id" Width="701px">
                                    <ItemStyles>
                                        <eo:GridItemStyleSet>
                                            <ItemStyle CssText="background-color: #FAFAFA" />
                                            <ItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem; " />
                                            <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                            <AlternatingItemHoverStyle CssText="font-family: 'Ubuntu'; font-size: 12px; font-weight: 500;  text-align: left; height: 30px !important; background: rgba(28, 148, 137, 0.32); color: #FFFFFF; padding: 0.3rem 0.3rem 0.3rem 0.5rem;" />
                                            <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                            <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                        </eo:GridItemStyleSet>
                                    </ItemStyles>
                                    <ColumnHeaderTextStyle CssText="" />
                                    <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                                    <Columns>
                                        <eo:StaticColumn AllowSort="True" DataField="Data" HeaderText="Data do Exame" 
                                            Name="DataExame" ReadOnly="True" SortOrder="Ascending" Text="" Width="140">
                                            <CellStyle 
                                                CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="Tipo" HeaderText="Tipo" 
                                            Name="Tipo" ReadOnly="True" Width="290">
                                            <CellStyle CssText="text-align:center" />
                                        </eo:StaticColumn>
                                        <eo:StaticColumn AllowSort="True" DataField="Descricao" HeaderText="Resultado" 
                                            Name="Tipo" ReadOnly="True" Width="220">
                                            <CellStyle CssText="text-align:center" />
                                        </eo:StaticColumn>                                       
                                    </Columns>
                                    <columntemplates>
                                        <eo:TextBoxColumn>
                                            <TextBoxStyle CssText="BORDER-RIGHT: #7f9db9 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #7f9db9 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8.75pt; PADDING-BOTTOM: 1px; MARGIN: 0px; BORDER-LEFT: #7f9db9 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #7f9db9 1px solid; FONT-FAMILY: Tahoma" />
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
                                    </columntemplates>
                                    <FooterStyle CssText="padding-bottom:4px;padding-left:4px;padding-right:4px;padding-top:4px;" />
                                </eo:Grid>
                                <br />
                                <asp:Label ID="lblTotRegistros" runat="server" CssClass="tituloLabel" Text="Label"></asp:Label>
                                
                                </eo:PageView>
                                
                          


                                <%-- Digitalização --%>
                                <eo:PageView ID="Pageview6" runat="server" Width="1050px">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="lblAnotacoes0" runat="server" CssClass="tituloLabel">Arquivo do Prontuário ASO :</asp:Label>
                                                <asp:TextBox ID="txt_Arq" runat="server" CssClass="texto form-control form-control-sm"
                                                    ReadOnly="True" Rows="4" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <din class="row">
                                            <div class="col-md-6 gx-3 gy-2">
                                                <asp:Label ID="Label6" runat="server" CssClass="tituloLabel">Inserir / Modificar Arquivo de Prontuário ASO :</asp:Label>
                                                <asp:FileUpload ID="File1" runat="server" ClientIDMode="Static" CssClass="texto form-control" />
                                            </div>
                                            <div class="col-md-3 gx-3 mt-3 pt-1">
                                                
                                            </div>
                                        </din>
                                    </div>
        
                                  
                                    <asp:Button ID="cmd_PDF" OnClick="cmd_PDF_Click" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Abrir PDF" Visible="False"  />
                                    <asp:Button ID="cmd_Imagem" OnClick="cmd_Imagem_Click" runat="server" BackColor="#FFCC99" Font-Bold="True" Font-Size="X-Small" Font-Strikeout="False" Text="Visualizar Prontuário" Visible="False" />
                                    <asp:Image ID="ImgFunc" runat="server" BorderColor="#660033" BorderStyle="Inset" BorderWidth="2px" Height="545px" Visible="False" Width="428px" />
                                    <asp:Label ID="lbl_Path" runat="server" Visible="False"></asp:Label>
                                </eo:PageView>




                                <%-- Anamnese Dinânica --%>
                                <eo:PageView ID="Pageview7" runat="server">

                                    
                                    <eo:Grid ID="gridAnamnese" runat="server" ColumnHeaderAscImage="00050403" 
                                            ColumnHeaderDescImage="00050404" 
                                            FixedColumnCount="1" GridLineColor="240, 240, 240" 
                                            GridLines="Both" Height="445px" Width="800px" ColumnHeaderDividerOffset="6" 
                                            ColumnHeaderHeight="30" ItemHeight="30" KeyField="IdAnamneseExame"  
                                            ClientSideOnItemCommand="OnItemCommand" FullRowMode="False" CssClass="grid" >
                                        <ItemStyles>
                                            <eo:GridItemStyleSet>
                                                <ItemStyle CssText="background-color: #FAFAFA" />
                                                <AlternatingItemStyle CssText="background-color: #F2F2F2;" />
                                                <SelectedStyle CssText="border-bottom-style:solid;border-left-style:solid;border-right-style:solid;border-top-style:solid;" />
                                                <CellStyle CssText="padding: 0.3rem 0.3rem 0.3rem 0.5rem; height: 30px !important;" />
                                            </eo:GridItemStyleSet>
                                        </ItemStyles>
                                        <ColumnHeaderTextStyle CssText="" />
                                        <ColumnHeaderStyle CssClass="tabelaC colunas"/>
                                        <Columns>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseExame" Name="IdAnamneseExame" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdExameBase" Name="IdExameBase" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="IdAnamneseDinamica" Name="IdAnamneseDinamica" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="" AllowSort="True" 
                                                DataField="Peso" Name="Peso" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="0">
                                                <CellStyle CssText="background-color:#ffffcc;font-family:Tahoma;font-weight:bold;text-align:center" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Sistema" AllowSort="True" 
                                                DataField="Sistema" Name="Sistema" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="130">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;font-weight:bold;text-align:left;" />
                                            </eo:StaticColumn>

                                            <eo:StaticColumn HeaderText="Questao" AllowSort="True" 
                                                DataField="Questao" Name="Questao" ReadOnly="True" 
                                                SortOrder="Ascending" Text="" Width="425">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:left;" />
                                            </eo:StaticColumn>

                                            

                                            <eo:CustomColumn HeaderText="Resultado" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                Width="72" DataType="String"  ClientSideEndEdit="on_end_edit" ClientSideBeginEdit="on_begin_edit"  ClientSideGetText="on_column_gettext" >
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:Center;" />           
                                               <EditorTemplate>
				                                                <select id="grade_dropdown"  >
                                                                    <option>Não</option>
					                                                <option>Sim</option>					                                                
				                                                </select>
			                                    </EditorTemplate>
                                            </eo:CustomColumn>

                                            
                                            <eo:TextBoxColumn HeaderText="Obs" AllowSort="True" 
                                                DataField="Obs" Name="Obs" ReadOnly="False" 
                                                SortOrder="Ascending"  Width="145">
                                                <CellStyle CssText="font-family:Tahoma;font-size:7pt;text-align:left;" />
                                            </eo:TextBoxColumn>


<%--                                            <eo:StaticColumn HeaderText="Resultado" AllowSort="True" 
                                                DataField="Resultado" Name="Resultado" ReadOnly="False" 
                                                SortOrder="Ascending" Text="" Width="75">
                                                <CellStyle CssText="font-family:Tahoma;font-size:6pt;text-align:Center;" />           
                                            </eo:StaticColumn>--%>
  
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

                                        <asp:Button ID="cmd_Anamnese"  onclick="cmd_Anamnese_Click"  runat="server" tabIndex="1" Text="Salvar Alterações na Anamnese" CssClass="btn" />
                                        
                                    <asp:LinkButton ID="lkbAnamnese" runat="server" SkinID="BoldLinkButton" Width="91px" Visible="False"><img border="0" src="img/print.gif"> Anamnese</img></asp:LinkButton>
                                        
                                </eo:PageView>




                                <%-- Antecedentes --%>
                                <eo:PageView ID="Pageview8" runat="server" Width="1050px">

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12 mt-2 gx-3 gy-2">
                                                <asp:Label ID="Label3326" runat="server" Font-Bold="True" CssClass="tituloLabel">Penúltimo Emprego</asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col gx-3 gy-2">
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label22" runat="server" CssClass="tituloLabel">Empresa:</asp:Label>
                                                        <asp:TextBox ID="txt_Penultima_Empresa" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label41" runat="server" CssClass="tituloLabel">Função:</asp:Label>
                                                        <asp:TextBox ID="txt_Penultima_Funcao" runat="server" AutoPostBack="True" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label42" runat="server" CssClass="tituloLabel">Tempo:</asp:Label>
                                                        <asp:TextBox ID="txt_Penultima_Tempo" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-12 mt-2 gx-3 gy-2">
                                                <asp:Label ID="Label3327" runat="server" Font-Bold="True" CssClass="tituloLabel">Último Emprego</asp:Label>
                                            </div>
                                            <div class="row">
                                                <div class="col gx-3 gy-2">
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label43" runat="server" CssClass="tituloLabel">Empresa:</asp:Label>
                                                        <asp:TextBox ID="txt_Ultima_Empresa" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label44" runat="server" CssClass="tituloLabel">Função:</asp:Label>
                                                        <asp:TextBox ID="txt_Ultima_Funcao" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 gx-3 gy-2">
                                                        <asp:Label ID="Label45" runat="server" CssClass="tituloLabel">Tempo:</asp:Label>
                                                        <asp:TextBox ID="txt_Ultima_Tempo" runat="server" CssClass="texto form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </eo:PageView>

            </eo:MultiPage>
        </div>

        <%-- botões finais --%>

        <div class="col-11">
            <div class="row">

                <div class="text-center mb-3">
                    <asp:Label ID="lblAusente" runat="server" CssClass="texto">Se caso o empregado não compareceu e o Exame não foi realizado,</asp:Label>
                    <asp:LinkButton ID="lbtAusente" runat="server" SkinID="BoldLinkButton">clique aqui</asp:LinkButton>
                </div>

                <div class="text-center mb-3">
                    <asp:Button ID="btnOK" onclick="btnOK_Click"  runat="server" tabIndex="1" Text="Gravar" CssClass="btn" />
                    <asp:Button ID="btnExcluir" onclick="btnExcluir_Click" runat="server" Text="Excluir" CssClass="btn"/>
                    <asp:LinkButton ID="lkbASO" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding: .3rem;"> ASO</img></asp:LinkButton>
                    <asp:LinkButton ID="lkbPCI" runat="server" SkinID="BoldLinkButton" CssClass="btn"><img border="0" src="Images/printer.svg" style="padding: .3rem;"> Ficha Clínica</img></asp:LinkButton>
                </div>

                <div class="text-start">
                    <asp:Button ID="cmd_Voltar" onclick="cmd_Voltar_Click"  runat="server" CssClass="btn2" Text="Voltar" />
                </div>

            </div>
        </div>
        
        <input id="txtAuxAviso" type="hidden" runat="server"/>
        <input id="txtCloseWindow" type="hidden" runat="server"/>
        <input id="txtExecutePost" type="hidden" runat="server"/>
        <input id="txtAuxiliar" type="hidden" runat="server"/>
        <%-- </igmisc:WebAsyncRefreshPanel>--%>
                     
            
        
    </eo:CallbackPanel>
    
        
         <eo:MsgBox ID="MsgBox1" runat="server" BackColor="#47729F" ControlSkinID="None"  OnButtonClick="MsgBox1_ButtonClick"
        HeaderHtml="Dialog Title" Height="100px" Width="250px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>
         <eo:MsgBox ID="MsgBox2" runat="server" BackColor="#47729F" ControlSkinID="None"  OnButtonClick="MsgBox1_ButtonClick"
        HeaderHtml="Dialog Title" Height="350px" Width="650px">
        <HeaderStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #ffbf00 3px solid; padding-left: 4px; font-weight: bold; font-size: 11px; padding-bottom: 2px; color: white; padding-top: 2px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <ContentStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        <FooterStyleActive CssText="border-right: #22456a 1px solid; padding-right: 4px; border-top: #7d97b6 1px solid; padding-left: 4px; border-left-width: 1px; font-size: 11px; border-left-color: #728eb8; padding-bottom: 4px; color: white; padding-top: 4px; border-bottom: #22456a 1px solid; font-family: verdana" />
        </eo:MsgBox>

        </div>
    </div>
</asp:Content>



