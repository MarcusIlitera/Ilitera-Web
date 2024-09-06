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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Ilitera.Common;
using Ilitera.PCMSO.Report;
using Ilitera.Opsa.Report;
using Entities;
using BLL;
using System.Collections;
using EO.Web;

namespace Ilitera.Net
{
    public partial class EquipCalibracao : System.Web.UI.Page
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


            hlkNovo.Visible = false;
            
                    
            
         

            if (!IsPostBack)
            {

                grd_Clinicos.DataSource = DSQuestionario();
                grd_Clinicos.DataBind();

                hlkNovo.Visible = true;
                hlkNovo.NavigateUrl = "javascript:void(window.location.href ='EquipCalibracao_Det.aspx?IdEquipamentoCalibracao=0&Acao=E')";


                // DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
                // + " AND IsFromWeb=0"
                // + " ORDER BY DataPcmso DESC");   
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
            ////base.InicializaWebPageObjects();
            //StringBuilder st = new StringBuilder();

            ////st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");


            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);

        }



        private DataSet DSQuestionario()
        {

            Ilitera.Data.Clientes_Clinicas xEq = new Ilitera.Data.Clientes_Clinicas();

            DataSet rDs = xEq.Trazer_Equipamentos(System.Convert.ToInt32(Session["Empresa"].ToString()));

            //DataSet zDs = new Ilitera.Opsa.Data.EquipamentoCalibracao().Get(" nId_Empr=" + Session["Empresa"].ToString() + " ORDER BY Equipamento ");

            //return zDs;

            return rDs;

        }





        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("~\\ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

        protected void btnFichaCompleta_Click(object sender, EventArgs e)
        {

        }


        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {
            //Check whether it is from our client side
            //JavaScript call
            string zAcao = "V";


            if (e.CommandName.ToString().Trim() == "8")
                zAcao = "E";  //editar
            else
                zAcao = "V";  //visualizar


            //Session["Empregado"] = e.Item.Key.ToString();
            //Session["NomeEmpregado"] = e.Item.Cells[0].Value.ToString();
           // string zTipo = Request["Tipo"].ToString().Trim();

         
            Response.Redirect("EquipCalibracao_Det.aspx?IdEquipamentoCalibracao=" + e.Item.Key.ToString() + "&Acao=" + zAcao);
          

        }



        protected void cmd_Gerar_CSV_Click(object sender, EventArgs e)
        {


            string xFile = "Relat_Equipamentos_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
            string myStringWebResource = "I:\\temp\\" + xFile;
            string zLinha = "";

            Ilitera.Data.Clientes_Clinicas xEq = new Ilitera.Data.Clientes_Clinicas();

            DataSet zDs = xEq.Trazer_Equipamentos_Completo(System.Convert.ToInt32(Session["Empresa"].ToString()));

            TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

            zLinha = "IdEquipamento_Calibracao;Dt_Aquisicao;Equipamento;Tipo_Equipamento;Fabricao;Numero_Serie;Localizacao;Dt_prox_Monitoramento;Dt_Ultima_Manutencao;Modelo;Certificado;Assistencia_Tecnica;Plano_Manut_Preventiva;TAG;Faixa_Utilizacao;Setor;Tipo_Monitoramento";
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


    }
}
