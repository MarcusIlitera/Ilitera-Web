using System;
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
using System.Collections;
using System.Collections.Generic;

using Entities;
using BLL;
using System.IO;
using Ilitera.Opsa.Report;

namespace Ilitera.Net.PCMSO
{
    public partial class Relatorio_Vacinas_Atrasadas : System.Web.UI.Page
    {
        private string xTipo="N";
        private string xId_Empresa;
        private string xCliente;
        private string xConsiderar;

        protected void Page_Load(object sender, System.EventArgs e)
		{
			InicializaWebPageObjects();
			//PreencheLabels("lblEmp", cliente.NomeAbreviado.ToString());

      

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


                //carregar combos de setor, funcao e cargo
                cmb_Setor.Items.Clear();

               


                Ilitera.Data.PPRA_EPI xGHE = new Ilitera.Data.PPRA_EPI();

                DataSet dS1 = xGHE.Carregar_Setores(System.Convert.ToInt32(Session["Empresa"].ToString()));

                cmb_Setor.DataSource = dS1;
                cmb_Setor.DataValueField = "nID_SETOR";
                cmb_Setor.DataTextField = "tNO_STR_EMPR";
                cmb_Setor.DataBind();

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
			//StringBuilder st = new StringBuilder();

			//st.Append("top.window.document.frames.conteudo.window.document.getElementById('txtIdEmpregado').value = '" + empregado.Id + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpregado').value = '" + Request["IdEmpregado"] + "';");
            //st.Append("top.window.document.getElementById('txtIdUsuario').value = '" + Request["IdUsuario"] + "';");
            //st.Append("top.window.document.getElementById('txtIdEmpresa').value = '" + Request["IdEmpresa"] + "';");
                            
            //this.ClientScript.RegisterStartupScript(this.GetType(), "IdEmpregado", st.ToString(), true);
            //btnFichaCompleta.Attributes.Add("onClick", "addItem(centerWin('../DadosEmpresa/FichaCompleta.aspx?IdEmpregado=" + Request["IdEmpregado"] + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdUsuario=" + Request["IdUsuario"] + "',560,320,\'FichaCompleta\'),\'Todos\'); Reload();");
                        
		}




        protected void btnemp_Click(object sender, EventArgs e)
        {
            Guid strAux = Guid.NewGuid();
            string xEmp = "1";
            string xTipoRel = "C";
            //string xConsiderar = "0";
            string xSetor = "0";


            xEmp = cmb_Empresa.SelectedValue.ToString().Trim();


            if (chk_Setor.Checked == true)
            {
                if (cmb_Setor.SelectedIndex >= 0) xSetor = cmb_Setor.SelectedValue.ToString().Trim();
            }



            //Session["Exames"] = xExames;
            if (chk_CSV.Checked == true)
            {
                xId_Empresa = Session["Empresa"].ToString();
                xCliente = Session["NomeEmpresa"].ToString();
                xConsiderar = xSetor;



                string xFile = "Relat_Vacinas_Atrasadas_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";

                DataSet zDs;

                zDs = new DataSourceVacinas().GetGuiasConv(System.Convert.ToInt32(xId_Empresa), xCliente, System.Convert.ToInt16(xEmp), "A", xConsiderar);

                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                //zLinha = "Colaborador;Admissao;Matricula; ;Funcao; ; ;Setor"; //Cabeçalho igual do relatório em PDF
                zLinha = "Colaborador;Admissao;Matricula;Tipo de vacina;Dose;Data da vacinação;Setor"; //Cabeçalho mais detalhado
                tw.WriteLine(zLinha);

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    zLinha = "";

                    for (int zAux = 0; zAux < zDs.Tables[0].Columns.Count; zAux++)
                    {
                        zLinha = zLinha + zDs.Tables[0].Rows[zCont][zAux].ToString().Trim().Replace(";", " / ") + ";";
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
            else
            {
                OpenReport("DadosEmpresa", "RelatorioVacinas.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                   + "&IdEmpresa=" + Session["Empresa"].ToString() + "&Data1=01/01/2001&Data2=31/12/2001&xTipo=" + xTipo + "&xEmp=" + xEmp + "&TipoRel=" + xTipoRel + "&xConsiderar=" + xSetor + "&NomeCliente=" + Session["NomeEmpresa"].ToString(), "RelatorioVacinas", true);
            }
        }




        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, false);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
            else if (valueProcess.Equals("Local"))
            {
                if (useDirectoryForLocalProcess)
                {
                    //st.Append("void(window.open('../" + directory + "/" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
        }


        protected void rd_Analitico_CheckedChanged(object sender, EventArgs e)
        {

            //if (rd_Analitico.Checked == true) cmb_Empresa.Enabled = false;
            //else cmb_Empresa.Enabled = true;

        }

        protected void rd_Sumarizado_CheckedChanged(object sender, EventArgs e)
        {

            //if (rd_Analitico.Checked == true) cmb_Empresa.Enabled = false;
            //else cmb_Empresa.Enabled = true;

        }

        protected void chk_Setor_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Setor.Checked == true) cmb_Setor.Enabled = true;
            else cmb_Setor.Enabled = false;
        }

        protected void chk_PDF_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chk_CSV_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
