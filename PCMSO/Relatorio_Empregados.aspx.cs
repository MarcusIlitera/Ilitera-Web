using System;
using System.Data;
using System.Text;
using System.IO;
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


namespace Ilitera.Net.PCMSO
{
    public partial class Relatorio_Empregados : System.Web.UI.Page
    {


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

            //if (Validar_Data(txt_Data.Text.Trim()) == false)
            //{
            //    return;
            //}

            //if (Validar_Data(txt_Data2.Text.Trim()) == false)
            //{
            //    return;
            //}

            Guid strAux = Guid.NewGuid();

            //Session["Exames"] = xExames;

            string xTipo = "A";
            string xEmp = "1";
            string xStatus = "1";
            string xApenasDeficiencia = "N";
            string xAtivosEm = "0";
            string xSemEmail = "N";
            string xInconsistencia = "N";
            string xClassif = "N";

            if (chk_Deficiencia.Checked == true)
                xApenasDeficiencia = "S";
            else
                xApenasDeficiencia = "N";

            if (chk_Classif.Checked == true)
                xClassif = "S";
            else
                xClassif = "N";

            
            if ( chk_Historico.Checked == true )
               xTipo = "H";
            else
               xTipo = "S";

            if (chk_eMail.Checked == true)
                xSemEmail = "S";
            else
                xSemEmail = "N";


            if ( chk_Inconsistencias.Checked == true )
            {
                if (opt_SemGHE.Checked == true) xInconsistencia = "G";
                else if (opt_Classif.Checked == true) xInconsistencia = "F";
                else if (opt_SemCPF.Checked == true) xInconsistencia = "C";
                else if (opt_Encerre.Checked == true) xInconsistencia = "D";
                else xInconsistencia = "N";
            }


            xEmp = cmb_Empresa.SelectedValue.ToString().Trim();

            xStatus = cmb_Status.SelectedValue.ToString().Trim();

            if ( chk_Ativos.Checked == true )
            {
                if ( Validar_Data( txt_Ativos.Text ) == false )
                {
                    return;
                }
                xAtivosEm = txt_Ativos.Text.Trim();
            }

            if (chk_CSV.Checked == true)
            {


                string zFiltro = "";

                if (xEmp == "1")   //empresa
                {
                    zFiltro = "  " + Session["Empresa"].ToString() + "  ";
                }
                else if (xEmp == "2")   //empresa            
                {
                    zFiltro = "   select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idjuridica = " + Session["Empresa"].ToString() + " or idjuridicapai = " + Session["Empresa"].ToString() + "  ";
                }
                else if (xEmp == "3")   //empresa            
                {
                    zFiltro = "  select idpessoa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idgrupoempresa in ( select idgrupoempresa from " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.juridica where idpessoa = " + Session["Empresa"].ToString() + " )  ";
                }
                else
                {
                    zFiltro = "  " + Session["Empresa"].ToString() + "  ";
                }



                string xFile = "Relat_Colaboradores_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";

                Ilitera.Opsa.Report.DataSourceColaboradores xRep = new Ilitera.Opsa.Report.DataSourceColaboradores();

                DataSet zDs = xRep.GetClientesColaboradores2( System.Convert.ToInt32( Session["Empresa"].ToString()), txt_Data.Text, "", xEmp, zFiltro, xStatus, xTipo, xApenasDeficiencia, xAtivosEm, xSemEmail, xInconsistencia, xClassif);

                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "Colaborador;Matricula;GHE;Setor;Função;Sexo;Admissão;Nascimento;Idade;Status;Empresa;Data_Relatorio;Deficiência;CPF;email;RG;Inicio_Funcao;Ini_Funcao";
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
            else
            {
                
                OpenReport("DadosEmpresa", "RelatorioColaboradores.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    + "&IdEmpresa=" + Session["Empresa"].ToString() + "&Defic=" + xApenasDeficiencia + "&Data1=" + txt_Data.Text + "&Data2=" + txt_Data2.Text + "&NomeCliente=" + Session["NomeEmpresa"].ToString() + "&tipo=" + xTipo + "&Emp=" + xEmp + "&Sit=" + xStatus + "&Ativosem=" + xAtivosEm + "&SemEmail=" + xSemEmail + "&xInconsistencia=" + xInconsistencia + "&Classif=" + xClassif, "RelatorioExame", true);
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

        protected void chk_Ativos_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Ativos.Checked == true) txt_Ativos.Enabled = true;
            else txt_Ativos.Enabled = false;

        }


        protected void chk_PDF_CheckedChanged(object sender, EventArgs e)
        {


        }

        protected void chk_CSV_CheckedChanged(object sender, EventArgs e)
        {


        }
    }

}
