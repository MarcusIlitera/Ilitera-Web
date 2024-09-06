using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ilitera.Opsa.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Ilitera.Common;
using Entities;
using BLL;
using System.Collections;

namespace Ilitera.Net.ControleEPI
{
    public partial class Fornecimento_Lista : System.Web.UI.Page
    {

        protected Prestador prestador = new Prestador();
        protected Ilitera.Common.Usuario usuario = new Ilitera.Common.Usuario();
        protected Cliente cliente = new Cliente();
        protected Ilitera.Opsa.Data.Empregado empregado = new Ilitera.Opsa.Data.Empregado();


        protected void Page_Load(object sender, System.EventArgs e)
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

            
            InicializaWebPageObjects();



            if (rd_Normal.Checked == true)
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            else
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI_Todos_CA(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            grd_Clinicos.DataBind();

        


            if (!IsPostBack)
            {
                System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
                txt_Data.Text = System.DateTime.Now.ToString("dd/MM/yyyy", ptBr);
            }

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void InicializaWebPageObjects()
        {
            //base.InicializaWebPageObjects();
            StringBuilder st = new StringBuilder();

            //st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            ////Check whether it is from our client side
            ////JavaScript call


            ////Session["Empregado"] = e.Item.Key.ToString();
            ////Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();

            //string zTipo = e.Item.Cells[1].Value.ToString();


            lbl_Id_Empregado.Text = e.Item.Key.ToString().Trim();
            lbl_Qtde.Text = e.Item.Cells[1].Value.ToString().Trim();
            lbl_IdEPIClienteCA.Text = e.Item.Cells[9].Value.ToString().Trim();
            lbl_IdCA.Text = e.Item.Cells[10].Value.ToString().Trim();


            if (e.Item.Cells[5].Value.ToString().Trim() == "0")
            {
                MsgBox1.Show("Ilitera.Net", "É necessário ajustar a Periodicidade do CA " + e.Item.Cells[3].Value.ToString().Trim(), null,
                              new EO.Web.MsgBoxButton("OK"));
                return;
            }


            if (Validar_Data(txt_Data.Text) == false)
            {
                return;
            }


            if (lbl_Qtde.Text == "" || lbl_Qtde.Text == "0")
            {
                MsgBox2.Show("Ilitera.Net", "Confirma a entrega de " + e.Item.Cells[2].Value.ToString().Trim() + " " + e.Item.Cells[3].Value.ToString().Trim() +
                                            " para o Colaborador " + e.Item.Cells[0].Value.ToString().Trim() + " com data de fornecimento " + txt_Data.Text + " ?", null,
                new EO.Web.MsgBoxButton("Não confirmo", null, "Não Confirmo"),
                new EO.Web.MsgBoxButton("Confirmo entrega - qtde 100", null, "Confirmo entrega - qtde 100"),
                new EO.Web.MsgBoxButton("Confirmo entrega - qtde 10", null, "Confirmo entrega - qtde 10"),
                new EO.Web.MsgBoxButton("Confirmo entrega - qtde 1", null, "Confirmo entrega - qtde 1"));
            }
            else
            {
                MsgBox2.Show("Ilitera.Net", "Confirma a entrega de " + e.Item.Cells[2].Value.ToString().Trim() + " " + e.Item.Cells[3].Value.ToString().Trim() +
                                            " para o Colaborador " + e.Item.Cells[0].Value.ToString().Trim() + " com data de fornecimento " + txt_Data.Text + " ?", null,
                new EO.Web.MsgBoxButton("Não confirmo", null, "Não Confirmo"),
                new EO.Web.MsgBoxButton("Confirmo entrega", null, "Confirmo entrega"));
            }
            


            return;

            //string zIdEmpregado = e.Item.Key.ToString();

            //Entities.Usuario zUser = (Entities.Usuario)Session["usuarioLogado"];


            //EPICAEntrega epiEntrega = new EPICAEntrega();
            //epiEntrega.Inicialize();

            //epiEntrega.IdEmpregado.Id = Convert.ToInt32(zIdEmpregado);
            //epiEntrega.DataRecebimento = DateTime.Now.Date;

            //epiEntrega.UsuarioId = zUser.IdUsuario;
            //epiEntrega.Save();


            //Int32 qtdentrega = System.Convert.ToInt32(e.Item.Cells[2].Value.ToString());

            //EPICAEntregaDetalhe epiEntregaDetalhe = new EPICAEntregaDetalhe();
            //epiEntregaDetalhe.Inicialize();


            //string zIdEPIClienteCA = e.Item.Cells[11].Value.ToString();
            //string zIdCA = e.Item.Cells[6].Value.ToString();


            //epiEntregaDetalhe.IdEPICAEntrega = epiEntrega;
            //epiEntregaDetalhe.IdEPIClienteCA.Id = Convert.ToInt32(zIdEPIClienteCA);
            //epiEntregaDetalhe.QtdEntregue = qtdentrega;

            //DataSet ds = new EPIClienteCA().GetEPIClienteCAExistente(Request.QueryString["IdEmpresa"].ToString(), zIdCA);

            //EPIClienteCA epica = new EPIClienteCA(Convert.ToInt32(zIdEPIClienteCA));
            //int qtdestoque = epica.QtdEstoque - qtdentrega;

            //if (qtdestoque < 0)
            //    epica.QtdEstoque = 0;
            //else
            //    epica.QtdEstoque -= qtdentrega;

            //epica.UsuarioId = zUser.IdUsuario;
            //epica.Save();

            //epiEntregaDetalhe.UsuarioId = zUser.IdUsuario;
            //epiEntregaDetalhe.Save();


            //DataSet zDs = new DataSet();


            //Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
            //zDs = xLista.Gerar_Lista_EPI(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
            //grd_Clinicos.DataSource = zDs;

            //grd_Clinicos.DataBind();

        }



        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }



        

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName)
        {
            return strOpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected static string strOpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }


        protected void MsgBox2_ButtonClick(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            //Use the command name to determine which
            //button was clicked
            if (e.CommandName == "Confirmo entrega")
            {
                Entrega_EPI();
            }
            else if ( e.CommandName == "Confirmo entrega - qtde 100" )
            {
                lbl_Qtde.Text = "100";
                Entrega_EPI();
            }
            else if ( e.CommandName == "Confirmo entrega - qtde 10" )
            {
                lbl_Qtde.Text = "10";
                Entrega_EPI();
            }
            else if ( e.CommandName == "Confirmo entrega - qtde 1" )
            {
                lbl_Qtde.Text = "1";
                Entrega_EPI();
            }
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


        protected void Entrega_EPI()
        {

            if (Validar_Data(txt_Data.Text) == false)
            {
                return;
            }



            string zIdEmpregado = lbl_Id_Empregado.Text;

            Entities.Usuario zUser = (Entities.Usuario)Session["usuarioLogado"];


            EPICAEntrega epiEntrega = new EPICAEntrega();
            epiEntrega.Inicialize();

            epiEntrega.IdEmpregado.Id = Convert.ToInt32(zIdEmpregado);

            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");
            epiEntrega.DataRecebimento = System.Convert.ToDateTime( txt_Data.Text,ptBr ); //DateTime.Now.Date;

            epiEntrega.UsuarioId = zUser.IdUsuario;
            epiEntrega.Save();


            Int32 qtdentrega = System.Convert.ToInt32(lbl_Qtde.Text);
                

            EPICAEntregaDetalhe epiEntregaDetalhe = new EPICAEntregaDetalhe();
            epiEntregaDetalhe.Inicialize();


            string zIdEPIClienteCA = lbl_IdEPIClienteCA.Text;
            string zIdCA = lbl_IdCA.Text;


            epiEntregaDetalhe.IdEPICAEntrega = epiEntrega;
            epiEntregaDetalhe.IdEPIClienteCA.Id = Convert.ToInt32(zIdEPIClienteCA);
            epiEntregaDetalhe.QtdEntregue = qtdentrega;
            epiEntregaDetalhe.Origem = "M";

            DataSet ds = new EPIClienteCA().GetEPIClienteCAExistente(Request.QueryString["IdEmpresa"].ToString(), zIdCA);

            EPIClienteCA epica = new EPIClienteCA(Convert.ToInt32(zIdEPIClienteCA));
            int qtdestoque = epica.QtdEstoque - qtdentrega;

            if (qtdestoque < 0)
                epica.QtdEstoque = 0;
            else
                epica.QtdEstoque -= qtdentrega;

            epica.UsuarioId = zUser.IdUsuario;
            epica.Save();

            epiEntregaDetalhe.UsuarioId = zUser.IdUsuario;
            epiEntregaDetalhe.Save();


            DataSet zDs = new DataSet();


            if (rd_Normal.Checked == true)
            {               
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            else
            {                
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI_Todos_CA(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }

        }

        protected void rd_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_Normal.Checked == true)
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            else
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI_Todos_CA(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            grd_Clinicos.DataBind();

        }

        protected void rd_CA_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_Normal.Checked == true)
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            else
            {
                DataSet zDs = new DataSet();
                Ilitera.Data.PPRA_EPI xLista = new Ilitera.Data.PPRA_EPI();
                zDs = xLista.Gerar_Lista_EPI_Todos_CA(Convert.ToInt32(Request.QueryString["IdEmpresa"]));
                grd_Clinicos.DataSource = zDs;
            }
            grd_Clinicos.DataBind();

        }
    }
}
