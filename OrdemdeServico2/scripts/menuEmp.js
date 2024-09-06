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
  mm_menu_0822161936_1.addMenuItem("Incluir","void(centerWin('IncluirEmpregado.aspx',590,450))");
   mm_menu_0822161936_1.hideOnMouseOut=true;
   mm_menu_0822161936_1.menuBorder=1;
   mm_menu_0822161936_1.menuLiteBgColor='#66cc66';
   mm_menu_0822161936_1.menuBorderBgColor='#009966';
   mm_menu_0822161936_1.bgColor='#006600';

  mm_menu_0808152343_0.writeMenus();
}