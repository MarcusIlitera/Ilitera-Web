using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Facade;
using Ilitera.Opsa.Data;
using System.Collections.Generic;
using Ilitera.Data;
using System.IO;

namespace Ilitera.Net
{
    public partial class ListaPendencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
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
                        Server.Transfer("~/Tratar_Excecao.aspx");
                        return;
                    }



                    if (Session["Empresa"] != null && Session["Empresa"].ToString().Trim() != String.Empty)
                    {
                        int idJuridica = Convert.ToInt32(Session["Empresa"]);
                        int idJuridicaPai = 0;

                        if (Session["JuridicaPai"] != null && Session["JuridicaPai"].ToString().Trim() != String.Empty)
                        {
                            idJuridicaPai = Convert.ToInt32(Session["JuridicaPai"]);
                        }

                        PopularGrid();

                        if (Request.QueryString["liberar"] != null && Request.QueryString["liberar"].ToString() != String.Empty)
                        {
                            Session["Empregado"] = String.Empty;
                            Session["NomeEmpregado"] = String.Empty;

                            Response.Redirect("~/ListaEmpregados.aspx");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "alert('Para selecionar um empregado é necessário selecionar uma Empresa antes, você será redirecionado para a página de seleção de empresas.');", true);
                        Response.Redirect("~/ListaEmpresas2.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }



        private void PopularGrid()
        {

            Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();

            string xData = txt_Data1.Text.Trim();
            string xData2 = txt_Data2.Text.Trim();

            string xTodasEmpresas = "N";

            if (chk_Empresas.Checked == true) xTodasEmpresas = "S";

            
            string zFiltro = "";

            if (chk_Empresas.Checked == false)   //empresa
            {
                zFiltro = Session["Empresa"].ToString().Trim() + "  ";
            }            
            else 
            {
                zFiltro = "  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + Session["Empresa"].ToString().Trim() + " )  and idpessoa in ( select idpessoa from pessoa (nolock) where isinativo = 0  )   ";
            }



            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

            //if (Ilitera.Data.SQLServer.EntitySQLServer.xDB2.IndexOf("Daiti") > 0)
            //{
            //UltraWebGridPendencias.DataSource = xAnexos.Lista_Exames_Pendentes(xData, Session["NomeEmpresa"].ToString(), txt_Nome.Text.Trim(), chk_Todos.Checked);
            UltraWebGridPendencias.DataSource = xAnexos.Lista_Exames_Pendentes(xData, zFiltro, txt_Nome.Text.Trim(), !chk_Vencto.Checked, xData2, xTodasEmpresas, usuario.IdPessoa);

            //}
            //else
            //{
            //    UltraWebGridPendencias.DataSource = xAnexos.Lista_Exames_Pendentes(xData, ConfigurationManager.AppSettings["Empresa"].ToString(), txt_Nome.Text.Trim(), chk_Todos.Checked);
            //}

            UltraWebGridPendencias.DataBind();

        }


        protected void chk_Todos_CheckedChanged(object sender, EventArgs e)
        {
            PopularGrid();
        }



        protected void txt_Nome_TextChanged(object sender, EventArgs e)
        {
            //gridEmpregados.Behaviors.Paging.PageIndex = 0;
            //Session["Pagina"] = "0";

            //Carregar_Funcionarios();
            PopularGrid();
        }

        protected void cmd_Busca_Click(object sender, EventArgs e)
        {

            if ( chk_Vencto.Checked == true )
            {

                if (Validar_Data(txt_Data1.Text.Trim()) == false)
                {
                    return;
                }

                if (Validar_Data(txt_Data2.Text.Trim()) == false)
                {
                    return;
                }

            }


            PopularGrid();

        }

        protected void UltraWebGridPendencias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void UltraWebGridPendencias_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {

            PopularGrid();
            UltraWebGridPendencias.PageIndex = e.NewPageIndex;
            UltraWebGridPendencias.DataBind();


        }

        protected void cmd_CSV_Click(object sender, EventArgs e)
        {

            string xFile = "Relat_Absent_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;
            string zLinha = "";


            if (chk_Vencto.Checked == true)
            {

                if (Validar_Data(txt_Data1.Text.Trim()) == false)
                {
                    return;
                }

                if (Validar_Data(txt_Data2.Text.Trim()) == false)
                {
                    return;
                }

            }


            string xTodasEmpresas = "N";

            if (chk_Empresas.Checked == true) xTodasEmpresas = "S";


            string zFiltro = "";

            if (chk_Empresas.Checked == false)   //empresa
            {
                zFiltro = Session["Empresa"].ToString().Trim() + "  ";
            }
            else
            {
                zFiltro = "  select idpessoa from juridica where idgrupoempresa in ( select idgrupoempresa from juridica where idpessoa = " + Session["Empresa"].ToString().Trim() + " )  ";
            }



            string xData = txt_Data1.Text.Trim();
            string xData2 = txt_Data2.Text.Trim();


            DataSet zDs = new DataSet();


            Ilitera.Data.PPRA_EPI xAnexos = new Ilitera.Data.PPRA_EPI();


            Entities.Usuario usuario = (Entities.Usuario)Session["usuarioLogado"];

            zDs = xAnexos.Lista_Exames_Pendentes(xData, zFiltro, txt_Nome.Text.Trim(), !chk_Vencto.Checked, xData2, xTodasEmpresas,usuario.IdPessoa);


            TextWriter tw = new StreamWriter(myStringWebResource, false, System.Text.Encoding.GetEncoding(1252));


           

            zLinha = "Empresa;Colaborador;Exame;DtProxima;GHE;Dt_Ultima;Ultimo_Espera;Complementar;UF;CPF";

            tw.WriteLine(zLinha);

            for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
            {
                zLinha = "";

                for (int zAux = 0; zAux < zDs.Tables[0].Columns.Count; zAux++)
                {
                    zLinha = zLinha + zDs.Tables[0].Rows[zCont][zAux].ToString().Trim() + ";";
                }

                tw.WriteLine(zLinha);
            }

            tw.Close();


            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            String Header = "Attachment; Filename=" + xFile;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
            System.IO.FileInfo Dfile = new System.IO.FileInfo(myStringWebResource); //HttpContext.Current.Server.MapPath(myStringWebResource));
            HttpContext.Current.Response.WriteFile(Dfile.FullName);
            HttpContext.Current.Response.End();

            MsgBox1.Show("Ilitera.Net", "Arquivo gerado.", null,
            new EO.Web.MsgBoxButton("OK"));


            return;


        }



        protected Boolean Validar_Data(string zData)
        {
            int zDia = 0;
            int zMes = 0;
            int zAno = 0;

            string Validar;
            bool isNumerical;
            int myInt;


            if (zData.Length != 10)
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inválida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "Mês Inválido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inválido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

        }

    }
}
