function mmLoadMenus() {
  if (window.mm_menu_0808152343_0) return;
  window.mm_menu_0808152343_0 = new Menu("root",127,14,"Verdana, Arial, Helvetica, sans-serif",9,"#000000","#000000","#66cc66","#99ff99","left","middle",0,0,500,-5,7,true,true,false,5,false,false);
  mm_menu_0808152343_0.addMenuItem("Cadastro&nbsp;Fun&ccedil;&atilde;o","void(centerWin('ListaFuncao.aspx',622,260))");
  mm_menu_0808152343_0.addMenuItem("Cadastro&nbsp;Setor","void(centerWin('ListaSetor.aspx',622,260))");
   mm_menu_0808152343_0.hideOnMouseOut=true;
   mm_menu_0808152343_0.menuBorder=1;
   mm_menu_0808152343_0.menuLiteBgColor='#66cc66';
   mm_menu_0808152343_0.menuBorderBgColor='#009966';
   mm_menu_0808152343_0.bgColor='#006600';
  window.mm_menu_0822161936_1 = new Menu("root",127,14,"Verdana, Arial, Helvetica, sans-serif",9,"#000000","#000000","#66cc66","#99ff99","left","middle",0,0,500,-5,7,true,true,false,5,true,false);
  mm_menu_0822161936_1.addMenuItem("Classifica&ccedil;&atilde;o&nbsp;Funcional","top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href='ListaPeriodoEmpregado.aspx'");
  mm_menu_0822161936_1.addMenuItem("Cadastro&nbsp;CATS","top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href='ListaCatEmpregado.aspx'");
  mm_menu_0822161936_1.addMenuItem("Editar","void(centerWin('EditarEmpregado.aspx',590,450))");
  mm_menu_0822161936_1.addMenuItem("Incluir","void(centerWin('IncluirEmpregado.aspx',590,450))");
   mm_menu_0822161936_1.hideOnMouseOut=true;
   mm_menu_0822161936_1.menuBorder=1;
   mm_menu_0822161936_1.menuLiteBgColor='#66cc66';
   mm_menu_0822161936_1.menuBorderBgColor='#009966';
   mm_menu_0822161936_1.bgColor='#006600';
  window.mm_menu_0822161932_4 = new Menu("root",99,14,"Verdana, Arial, Helvetica, sans-serif",9,"#000000","#000000","#66cc66","#99ff99","left","middle",0,0,500,-5,7,true,true,false,5,true,false);
  mm_menu_0822161932_4.addMenuItem("Prontu&aacute;rio&nbsp;M&eacute;dico","javascript:void(window.open('PCIEmpregado.aspx','localidade','scrollbars=yes,resizable=yes,toolbar=no,addressbar=no,menubar=no,width=800px,height=500px').focus());");
  mm_menu_0822161932_4.addMenuItem("PPP","javascript:void(window.open('PPPEmpregado.aspx','localidade','scrollbars=yes,resizable=yes,toolbar=no,addressbar=no,menubar=no,width=800px,height=500px').focus());");
  mm_menu_0822161932_4.addMenuItem("LTCAT","window.open('/ltcat.aspx', 'tela');");
  mm_menu_0822161932_4.addMenuItem("PPRA","window.open('/ppra.aspx', 'tela');");
   mm_menu_0822161932_4.hideOnMouseOut=true;
   mm_menu_0822161932_4.menuBorder=1;
   mm_menu_0822161932_4.menuLiteBgColor='#66cc66';
   mm_menu_0822161932_4.menuBorderBgColor='#009966';
   mm_menu_0822161932_4.bgColor='#006600';
  window.mm_menu_0822161935_2 = new Menu("root",81,14,"Verdana, Arial, Helvetica, sans-serif",9,"#000000","#000000","#66cc66","#99ff99","left","middle",0,0,500,-5,7,true,true,false,5,true,false);
  mm_menu_0822161935_2.addMenuItem("Clin&iacute;co","top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href='PastaExClinico.aspx';");
  mm_menu_0822161935_2.addMenuItem("Complementar","top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href='PastaExComplementar.aspx'");
  mm_menu_0822161935_2.addMenuItem("Audiom&eacute;trico","top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href='PastaExAudiometrico.aspx'");
   mm_menu_0822161935_2.hideOnMouseOut=true;
   mm_menu_0822161935_2.menuBorder=1;
   mm_menu_0822161935_2.menuLiteBgColor='#66cc66';
   mm_menu_0822161935_2.menuBorderBgColor='#009966';
   mm_menu_0822161935_2.bgColor='#006600';

  mm_menu_0822161935_2.writeMenus();
}

function RiscosLink()
{
	top.window.document.frames.conteudo.window.document.frames.SubDados.window.location.href="RiscosEPIs.aspx";
}
