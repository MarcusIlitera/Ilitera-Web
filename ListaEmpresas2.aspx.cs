using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using Facade;
using System.Configuration;
using System.Collections;


namespace Ilitera.Net
{
    public partial class ListaEmpresas2 : System.Web.UI.Page
    {
        static private string xBusca;

        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                string FormKey = this.Page.ToString().Substring(4);

                Ilitera.Common.Funcionalidade funcionalidade = new Ilitera.Common.Funcionalidade();
                funcionalidade.Find("ClassName='" + FormKey + "'");

                if (funcionalidade.Id == 0)
                    throw new Exception("Formulário não cadastrado - " + FormKey);

                Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];
                Ilitera.Common.Usuario.Permissao_Web(xUser.IdUsuario, funcionalidade.Id);
            }
            catch (Exception ex)
            {

                Session["Message"] = ex.Message;
                Session["ExceptionType"] = " ";
                Session["Source"] = " ";
                //Server.Transfer("~/Tratar_Excecao.aspx");

                MsgBox1.Show("Acesso ao Módulo", ex.Message, null,
                    new EO.Web.MsgBoxButton("OK"));

                return;
            }


            try
            {

                string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

                if (zUrl.IndexOf("ESSENCE") >= 0 || zUrl.IndexOf("PRAJNA") >= 0)
                {
                    lblGrupoEmpresa.Visible = true;
                    cmbGrupoEmpresa.Visible = true;

                    if (!Page.IsPostBack)
                        CarregarGrupoEmpresa();
                }
                else
                {
                    lblGrupoEmpresa.Visible = false;
                    cmbGrupoEmpresa.Visible = false;
                }

                Usuario user = (Usuario)Session["usuarioLogado"];
                Session["Pagina"] = "0";

                if (txt_Nome.Text.Trim() == "") Session["Filtro_Nome"] = "";

                if (txt_Matricula.Text.Trim() == "") Session["Filtro_Matricula"] = "";

                Session["Filtro_Empresa"] = "";
                Session["Filtro_Tipo"] = "";
                Session["Filtro_Data_De"] = "";
                Session["Filtro_Data_Ate"] = "";
                Session["Filtro_Setor"] = "";

                if (txt_Nome.Text.Trim() == "" && txt_Empresa.Text.Trim() == "" && txt_Matricula.Text == "")
                {
                    if (!Page.IsPostBack)
                        PopularGrid(user);
                }
                else
                {
                    if (txt_Empresa.Text.Trim() != "")
                    {
                        PopularGrid_Empresa(user);
                    }
                    else if (txt_Nome.Text.Trim() != "")
                    {
                        PopularGrid_Nome(user, txt_Nome.Text.Trim());
                    }
                    else
                    {
                        PopularGrid_Matricula(user, txt_Matricula.Text.Trim());
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }


        }


        [System.Security.Permissions.EnvironmentPermissionAttribute(System.Security.Permissions.SecurityAction.LinkDemand, Unrestricted = true)]

        private void CarregarGrupoEmpresa()
        {

            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

            cmbGrupoEmpresa.Items.Clear();


            //ArrayList xGrupos = new Ilitera.Common.GrupoEmpresa().Find(" Descricao is not null and idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa in ( select idpessoa from pessoa where isinativo = 0 ) )  order by Descricao ");
            ArrayList xGrupos = new Ilitera.Common.GrupoEmpresa().Find(
                            " Descricao is not null and idgrupoempresa in ( " +
                            " select distinct idgrupoempresa from juridica where idpessoa in " +
                            "( " +
                            " select idcliente from prestadorcliente (nolock)" +
                            " where idprestador in " +
                            "   (" +
                            "    select idprestador from prestador (nolock) where IdJuridicaPessoa in  " +
                            "     (" +
                            "       select IdJuridicaPessoa from juridicapessoa (nolock)" +
                            "       where idpessoa = " + usuario.IdPessoa + " " +
                            "      ) " +
                            "   ) " +
                            ") " +
                            ") " +
                            " order by descricao ");

            cmbGrupoEmpresa.Items.Add("- Todos Grupos");

            foreach (Ilitera.Common.GrupoEmpresa xGE in xGrupos)
            {
                cmbGrupoEmpresa.Items.Add(xGE.Descricao.Trim());
            }

            cmbGrupoEmpresa.SelectedIndex = 0;


        }

        private void PopularGrid(Usuario user)
        {

            string zEmpresa = "";

            zEmpresa = Retornar_Empresa_Filtro();

            //if (zEmpresa == "" && cmbGrupoEmpresa.SelectedIndex > 0)
            //    zEmpresa = cmbGrupoEmpresa.Items[cmbGrupoEmpresa.SelectedIndex].ToString().Trim();


            string zInativos = "T";

            if (rd_Ativos.Checked == true) zInativos = "A";
            else if (rd_Inativos.Checked == true) zInativos = "I";
            else zInativos = "T";


            string zOrdem = " ";

            if (rd_Descendente.Checked == true) zOrdem = "Desc";


            //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString
            //var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, ConfigurationManager.AppSettings["Empresa"].ToString());


            var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, zEmpresa, zInativos, zOrdem);


            //ENPRO quer que liste empresas fora do grupo,  e que tem nomes completamente diferentes
            string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

            if (zUrl.ToUpper().IndexOf("ENPRO") > 0)
            {
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "CPI", zInativos, zOrdem));
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "GGB", zInativos, zOrdem));
            }
            else if (zUrl.ToUpper().IndexOf("JAPAN") > 0 )
            {                
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "BRASILWAGEN", zInativos, zOrdem));
            }
            else if (zUrl.ToUpper().IndexOf("CAOA") > 0)
            {
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "CAOA", zInativos, zOrdem));
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "D21", zInativos, zOrdem));
            }
            else if (zUrl.ToUpper().IndexOf("MOINHOS") > 0)
            {
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "CORRECTA", zInativos, zOrdem));
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "MOINHO", zInativos, zOrdem));
            }
            else if (zUrl.ToUpper().IndexOf("MIRASSOL") > 0)
            {
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "MIRASSOL", zInativos, zOrdem));
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "M3", zInativos, zOrdem));
            }
            else if  (zUrl.ToUpper().IndexOf("BRASILWAGEN") > 0 )
            {
                retorno.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "JAPAN", zInativos, zOrdem));             
            }


            Session["Servidor_Web"] = ConfigurationManager.AppSettings["Servidor_Web"].ToString();



                      
            grd_Empresas.Items.Clear();
            grd_Empresas.DataSource = null;
            grd_Empresas.DataBind();

            grd_Empresas.Items.Clear();
            grd_Empresas.DataSource = retorno;
            grd_Empresas.DataBind();

         
        }


        protected void rd_Ascendente_CheckedChanged(object sender, EventArgs e)
        {
            grd_Empresas.Items.Clear();
            grd_Empresas.DataSource = null;
            grd_Empresas.DataBind();

            Usuario user = (Usuario)Session["usuarioLogado"];
            PopularGrid(user);
        }

        protected void rd_Descendente_CheckedChanged(object sender, EventArgs e)
        {
            grd_Empresas.Items.Clear();
            grd_Empresas.DataSource = null;
            grd_Empresas.DataBind();

            Usuario user = (Usuario)Session["usuarioLogado"];
            PopularGrid(user);
        }







        protected string Retornar_Empresa_Filtro()
        {
            string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

            string zEmpresa = "";


            //if (zUrl.IndexOf("PRAJNA") > 0 || zUrl.IndexOf("ESSENCE") > 0 || zUrl.IndexOf("BRASPRESS") > 0 || zUrl.IndexOf("MIRA") > 0 || zUrl.IndexOf("TECMAR") > 0 
            //    || zUrl.IndexOf("CPM") > 0 || zUrl.IndexOf("ANDIAMO") > 0 || zUrl.IndexOf("BANRISUL") > 0 || zUrl.IndexOf("ALLIED") > 0 || zUrl.IndexOf("ALATUR") > 0
            //    || zUrl.IndexOf("MOCRU") > 0 || zUrl.IndexOf("ITAKINI") > 0 || zUrl.IndexOf("ALATUR") > 0 || zUrl.IndexOf("KRONES") > 0 || zUrl.IndexOf("SONOVA") > 0)
            //{
            //    Img_Sistema.Visible = true;
            //    lbl_Sistema.Visible = true;
            //    lnk_Sistema.Visible = true;

            //    Usuario user = (Usuario)Session["usuarioLogado"];

            //    lnk_Sistema.NavigateUrl = "https://www.ilitera.net.br/essence2/Login.aspx?Nome=" + user.NomeUsuario + "&Senha=" + user.Senha;

            //}
            //else
            //{
            //    Img_Sistema.Visible = false;
            //    lbl_Sistema.Visible = false;
            //    lnk_Sistema.Visible = false;
            //}



                //primeiro colocar empresas que URL não bate com string para filtragem de empresas, ou franqueadas sem filtro por empresa
            if (zUrl.IndexOf("ILITERA") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("DAITI") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("PRIME") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("MITSUI") > 0)
            {
                zEmpresa = "BRASILEIRO";
            }
            else if (zUrl.IndexOf("SOMA") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("FOCUS") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("FOX") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("EY") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("STARBLOCK") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("SAFETY") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("PRO") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("ERGON") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("NOVO_VISUAL") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("MILANO") > 0)
            {
                zEmpresa = "MODELO";
            }
            else if (zUrl.IndexOf("GLOBAL") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("SDTURIN") > 0 || zUrl.IndexOf("SDTOURIN") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("GRAFENO") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("FIORE") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("MAPPAS") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("METODO") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("PRAJNA") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("ESSENCE") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("UNO") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("SHO") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("QTECK") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("LIFE") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("VIA") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("COTIA") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("SDMO") > 0)
            {
                zEmpresa = "MAQUIGERAL";
            }
            else if (zUrl.IndexOf("ALCATEL_ANCHIETA") > 0)
            {
                zEmpresa = "ANCHIETA";
            }
            else if (zUrl.IndexOf("ALCATEL_SP") > 0)
            {
                zEmpresa = "NOKIA";
            }
            else if (zUrl.IndexOf("RONA") > 0)
            {
                zEmpresa = "RO-NA";
            }
            else if (zUrl.IndexOf("ENPRO") > 0)
            {
                zEmpresa = "GARLOCK";
            }
            else if (zUrl.IndexOf("BEMPROMOTORA") > 0)
            {
                zEmpresa = "BEM ";
            }
            else if (zUrl.IndexOf("CPM") > 0)
            {
                zEmpresa = "CAPGEMINI";
            }
            else if (zUrl.IndexOf("BRASPRESS") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("SCJ") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("JOHNSON") > 0)
            {
                zEmpresa = "";
            }
            else if (zUrl.IndexOf("BOBS") > 0)
            {
                zEmpresa = "GRUPO BOB";
            }
            else if (zUrl.IndexOf("FORTKNOX") > 0)
            {
                zEmpresa = "FORT KNOX";
            }
            else if (zUrl.IndexOf("DECATHLON") > 0)
            {
                zEmpresa = "IGUASPORT";
            }         
            else if (zUrl.IndexOf("MIRA") > 0 && zUrl.IndexOf("MIRASSOL") < 0)
            {
                zEmpresa = "MIRA -";
            }
            else
            {
                string zAux = "";

                for (int zCont = zUrl.IndexOf("/LISTAEMPRESAS2") - 1; zCont > 1; zCont--)
                {

                    if (zUrl.Substring(zCont, 1) == "/")
                        break;

                    zAux = zUrl.Substring(zCont, 1) + zAux;

                }

                if (zAux.IndexOf("LOCALHOST") >= 0)
                    zAux = "";

                zEmpresa = zAux;

            }

            return zEmpresa;

        }




        protected void grd_Empresas_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call

            string s = string.Empty;
            s += "Row Index:" + e.Item.Index.ToString();
            //s += "<br />1:" + e.Item.Cells[1].Value.ToString();
            //s += "<br />2:" + e.Item.Cells[2].Value.ToString();
            //s += "<br />3:" + e.Item.Cells[3].Value.ToString();


            if (e.CommandName.Trim() == "4")  //dados cadastrais
            {

                Session["Empresa"] = e.Item.Key.ToString().Replace("....", ""); ;
                Session["JuridicaPai"] = e.Item.Cells[1].Value.ToString();

                //Session["Filtro_Nome"] = "";
                Session["Filtro_Tipo"] = "";

                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = e.Item.Cells[0].Value.ToString().Replace("....", ""); ;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;

                string xPreposto = EmpresaFacade.RetornarDadosPreposto(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto"] = xPreposto;

                string xPreposto2 = EmpresaFacade.RetornarDadosPreposto2(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto2"] = xPreposto2;


                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C03.pdf";
                Response.Redirect("VisualizarDadosEmpresa.aspx");


            }
            else if (e.CommandName.Trim() == "3")  //selecionar
            {

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                Session["Empresa"] = e.Item.Key.ToString().Replace("....", ""); ;
                Session["JuridicaPai"] = e.Item.Cells[1].Value.ToString();

                //Session["Filtro_Nome"] = "";
                Session["Filtro_Tipo"] = "";

                Session["Empregado"] = String.Empty;
                Session["NomeEmpregado"] = String.Empty;
                Session["NomeEmpresa"] = e.Item.Cells[0].Value.ToString().Replace("....", ""); ;
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;

                string xPreposto = EmpresaFacade.RetornarDadosPreposto(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto"] = xPreposto;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                string xPreposto2 = EmpresaFacade.RetornarDadosPreposto2(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto2"] = xPreposto2;


                //Response.Redirect("Default.aspx");
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04.pdf";
                Response.Redirect("ListaEmpregados.aspx");

            }
        }



        protected void grd_Empresas_Nome_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call

            string s = string.Empty;
            s += "Row Index:" + e.Item.Index.ToString();
            //s += "<br />1:" + e.Item.Cells[1].Value.ToString();
            //s += "<br />2:" + e.Item.Cells[2].Value.ToString();
            //s += "<br />3:" + e.Item.Cells[3].Value.ToString();


           if (e.CommandName.Trim() == "4")  //selecionar
            {

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


                string zInativos = "";

                if (rd_Ativos.Checked == true) zInativos = "A";
                else if (rd_Inativos.Checked == true) zInativos = "I";
                else zInativos = "T";

                Session["Filtro_Tipo"] = zInativos;


                Session["Empresa"] = e.Item.Key.ToString().Replace("....","");
                Session["JuridicaPai"] = "0";
                

                Session["Empregado"] = e.Item.Cells[3].Value.ToString(); 
                Session["NomeEmpregado"] = e.Item.Cells[1].Value.ToString();

                Session["NomeEmpresa"] = e.Item.Cells[0].Value.ToString().Replace("....", ""); 
                Session["Preposto"] = String.Empty;
                Session["Preposto2"] = String.Empty;

                string xPreposto = EmpresaFacade.RetornarDadosPreposto(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto"] = xPreposto;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                string xPreposto2 = EmpresaFacade.RetornarDadosPreposto2(Convert.ToInt32(Session["Empresa"]));
                Session["Preposto2"] = xPreposto2;

                
                //Response.Redirect("Default.aspx");
                Session["Ajuda"] = "~/Help/Manual_Novo_Ilitera_Net_C04.pdf";
                Response.Redirect("ListaEmpregados.aspx");

            }




        }

        protected void cmd_Busca_Click(object sender, EventArgs e)
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            txt_Empresa.Text = "";
            Session["Filtro_Empresa"] = "";

            Usuario user = (Usuario)Session["usuarioLogado"];

            Session["Pagina"] = "0";
            Session["Filtro_Nome"] = "";
            Session["Filtro_Matricula"] = "";
            Session["Filtro_Empresa"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Filtro_Data_De"] = "";
            Session["Filtro_Data_Ate"] = "";
            Session["Filtro_Setor"] = "";

            //if (txt_Nome.Text.Trim() == ""  && txt_Matricula.Text.Trim() == "")
            //{
            //    PopularGrid(user);
            //}
            //else
            //{
            //    if (txt_Nome.Text.Trim() != "")
            //        PopularGrid_Nome(user, txt_Nome.Text.Trim());
            //    else
            //        PopularGrid_Matricula(user, txt_Matricula.Text.Trim());
            //}
        }




        private void PopularGrid_Empresa(Usuario user)
        {

            string zEmpresa = "";
            string zInativos = "T";

            zEmpresa = Retornar_Empresa_Filtro();


            if (rd_Ativos.Checked == true) zInativos = "A";
            else if (rd_Inativos.Checked == true) zInativos = "I";
            else zInativos = "T";

            string zOrdem = " ";

            if (rd_Descendente.Checked == true) zOrdem = "Desc";


            //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString
            //var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, ConfigurationManager.AppSettings["Empresa"].ToString());
            //var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "PROXYON -");

            var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, zEmpresa,zInativos, zOrdem);

            Session["Servidor_Web"] = ConfigurationManager.AppSettings["Servidor_Web"].ToString();


            // montar filtro em cima de List
            // http://www.endswithsaurus.com/2008/11/navpanel-width-100-text-align-right.html
            //string Search = "AC";

            //var retorno2 = retorno.FindAll(BuscaNome);

            if (Session["Filtro_Empresa"].ToString().Trim() != "")
            {
                txt_Empresa.Text = Session["Filtro_Empresa"].ToString().Trim();
                Session["Filtro_Empresa"] = "";
            }

            xBusca = txt_Empresa.Text.Trim();
            Session["Filtro_Empresa"] = xBusca;


            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);

            grd_Empresas.Items.Clear();
            grd_Empresas.DataSource = null;
            grd_Empresas.DataBind();


            if (xBusca == "")
            {
                grd_Empresas.Items.Clear();
                grd_Empresas.DataSource = retorno;
                grd_Empresas.DataBind();
                retorno = null;
            }
            else
            {
                var retorno2 = retorno.FindAll(new Predicate<Entities.EmpresaDTO>(Checar_Nome));
                grd_Empresas.Items.Clear();
                grd_Empresas.DataSource = retorno2;
                grd_Empresas.DataBind();
                retorno = null;
                retorno2 = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }


        private static bool Checar_Nome(Entities.EmpresaDTO xEmpr)
        {
            if (xEmpr.NomeAbreviado.ToString().ToUpper().IndexOf(xBusca.ToString().ToUpper()) >= 0) { return true; } else { return false; }
        }


        private void PopularGrid_Nome(Usuario user, string xNome)
        {

            string zEmpresa = "";
            string zInativos = "";

            zEmpresa = Retornar_Empresa_Filtro();


            if (zEmpresa == "" && cmbGrupoEmpresa.SelectedIndex > 0)
                zEmpresa = cmbGrupoEmpresa.Items[cmbGrupoEmpresa.SelectedIndex].ToString().ToUpper().Trim();


            if (rd_Ativos.Checked == true) zInativos = "A";
            else if (rd_Inativos.Checked == true) zInativos = "I";
            else zInativos = "T";



            //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString
            //var retorno2 = EmpresaFacade.RetornarEmpresas_Nome(user.IdPrestador, 1, ConfigurationManager.AppSettings["Empresa"].ToString(), xNome);
            //var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "PROXYON -");


            ////var retorno2 = EmpresaFacade.RetornarEmpresas_Nome(user.IdPrestador, 1, zEmpresa, xNome);
            var retorno2 = EmpresaFacade.RetornarEmpresas_Nome2(user.IdPrestador, 1, zEmpresa, xNome, zInativos);

            ////ENPRO quer que liste empresas fora do grupo,  e que tem nomes completamente diferentes            
            //string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

            //if (zUrl.IndexOf("ENPRO") > 0)
            //{
            //    if ( xNome.ToUpper().IndexOf("CPI") >= 0 )     retorno2.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "CPI", zInativos) );

            //    if ( xNome.ToUpper().IndexOf("GGB") >= 0 )     retorno2.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "GGB", zInativos) );
            //}



            Session["Servidor_Web"] = ConfigurationManager.AppSettings["Servidor_Web"].ToString();
            Session["Filtro_Nome"] = xNome;


        

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);

            //for (int xCont = grd_Empresas.Items.Count; xCont > 0; xCont--)
            //{
            //    grd_Empresas.Items.RemoveAt(xCont-1);
            //}

            grd_Empresas.Visible = false;
            grd_EmpresasNome.Visible = true;

            grd_EmpresasNome.Items.Clear();
            grd_EmpresasNome.DataSource = null;
            grd_EmpresasNome.DataBind();

            grd_EmpresasNome.Items.Clear();
            grd_EmpresasNome.DataSource = retorno2;
            grd_EmpresasNome.DataBind();


        }


        private void PopularGrid_Matricula(Usuario user, string xMatricula)
        {

            string zEmpresa = "";
            string zInativos = "";

            zEmpresa = Retornar_Empresa_Filtro();

            if (zEmpresa == "" && cmbGrupoEmpresa.SelectedIndex > 0)
                zEmpresa = cmbGrupoEmpresa.Items[cmbGrupoEmpresa.SelectedIndex].ToString().ToUpper().Trim();


            if (rd_Ativos.Checked == true) zInativos = "A";
            else if (rd_Inativos.Checked == true) zInativos = "I";
            else zInativos = "T";



            //ConfigurationManager.ConnectionStrings["ConexaoIliteraOpsa"].ConnectionString
            //var retorno2 = EmpresaFacade.RetornarEmpresas_Nome(user.IdPrestador, 1, ConfigurationManager.AppSettings["Empresa"].ToString(), xNome);
            //var retorno = EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "PROXYON -");


            var retorno2 = EmpresaFacade.RetornarEmpresas_Matricula2(user.IdPrestador, 1, zEmpresa, xMatricula, zInativos);


            ////ENPRO quer que liste empresas fora do grupo,  e que tem nomes completamente diferentes            
            //string zUrl = HttpContext.Current.Request.Url.AbsoluteUri.ToString().ToUpper();

            //if (zUrl.IndexOf("ENPRO") > 0)
            //{
            //    if (xMatricula.ToUpper().IndexOf("CPI") >= 0) retorno2.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "CPI", zInativos));

            //    if (xMatricula.ToUpper().IndexOf("GGB") >= 0) retorno2.AddRange(EmpresaFacade.RetornarEmpresas(user.IdPrestador, 1, "GGB", zInativos));
            //}



            //Session["Servidor_Web"] = ConfigurationManager.AppSettings["Servidor_Web"].ToString();
            //Session["Filtro_Matricula"] = xMatricula;




            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);

            ////for (int xCont = grd_Empresas.Items.Count; xCont > 0; xCont--)
            ////{
            ////    grd_Empresas.Items.RemoveAt(xCont-1);
            ////}

            //grd_Empresas.Items.Clear();
            //grd_Empresas.DataSource = null;
            //grd_Empresas.DataBind();

            //grd_Empresas.Items.Clear();
            //grd_Empresas.DataSource = retorno2;
            //grd_Empresas.DataBind();

            Session["Servidor_Web"] = ConfigurationManager.AppSettings["Servidor_Web"].ToString();
            Session["Filtro_Matricula"] = xMatricula;
            


            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "DoPostBack", "__doPostBack(sender, e)", true);

            //for (int xCont = grd_Empresas.Items.Count; xCont > 0; xCont--)
            //{
            //    grd_Empresas.Items.RemoveAt(xCont-1);
            //}

            grd_Empresas.Visible = false;
            grd_EmpresasNome.Visible = true;

            grd_EmpresasNome.Items.Clear();
            grd_EmpresasNome.DataSource = null;
            grd_EmpresasNome.DataBind();

            grd_EmpresasNome.Items.Clear();
            grd_EmpresasNome.DataSource = retorno2;
            grd_EmpresasNome.DataBind();

        }





        protected void cmd_Busca_Emp_Click(object sender, EventArgs e)
        {

            txt_Nome.Text = "";
            Session["Filtro_Nome"] = "";

            //Usuario user = (Usuario)Session["usuarioLogado"];
            //PopularGrid_Empresa(user);                
        }

        protected void rd_Todos_CheckedChanged(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogado"];

            Session["Pagina"] = "0";
            Session["Filtro_Nome"] = "";
            Session["Filtro_Empresa"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Filtro_Data_De"] = "";
            Session["Filtro_Data_Ate"] = "";
            Session["Filtro_Setor"] = "";

            PopularGrid_Empresa(user);
        }

        protected void rd_Ativos_CheckedChanged(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogado"];

            Session["Pagina"] = "0";
            Session["Filtro_Nome"] = "";
            Session["Filtro_Empresa"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Filtro_Data_De"] = "";
            Session["Filtro_Data_Ate"] = "";
            Session["Filtro_Setor"] = "";

            PopularGrid_Empresa(user);
        }

        protected void rd_Inativos_CheckedChanged(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuarioLogado"];

            Session["Pagina"] = "0";
            Session["Filtro_Nome"] = "";
            Session["Filtro_Empresa"] = "";
            Session["Filtro_Tipo"] = "";
            Session["Filtro_Data_De"] = "";
            Session["Filtro_Data_Ate"] = "";
            Session["Filtro_Setor"] = "";

            PopularGrid_Empresa(user);
        }

        protected void cmbGrupoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Usuario user = (Usuario)Session["usuarioLogado"];
           // PopularGrid(user);
        }

    }
}
