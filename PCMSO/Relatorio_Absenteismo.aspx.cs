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
using System.IO;

using Entities;
using BLL;


namespace Ilitera.Net.PCMSO
{
    public partial class Relatorio_Absenteismo : System.Web.UI.Page
    {
        private string xTipo="";

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
                        throw new Exception("Formul�rio n�o cadastrado - " + FormKey);

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

            if (Validar_Data(txt_Data.Text.Trim()) == false)
            {
                return;
            }

            if (Validar_Data(txt_Data2.Text.Trim()) == false)
            {
                return;
            }


            Guid strAux = Guid.NewGuid();
            string xEmp = "1";
            string xTipoRel = "A";
            string xConsiderar = "0";
            string xStatus = "1";
            string xAtestados = "1";


            xConsiderar = cmb_Tipo.SelectedValue.ToString().Trim();
            xStatus = cmb_Status.SelectedValue.ToString().Trim();
            xEmp = cmb_Empresa.SelectedValue.ToString().Trim();

            xAtestados = cmb_Atestados.SelectedValue.ToString().Trim();



            if (rd_Sumarizado.Checked == true) xTipoRel = "S";
            else if (rd_Sumarizado_Empr.Checked == true) xTipoRel = "E";
            else xTipoRel = "A";



            //Session["Exames"] = xExames;

            if ( chk_CSV.Checked == true)
            {

                string xFile = "Relat_Absent_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";

                DataSet zDs = new DataSet();

                Ilitera.Data.Clientes_Funcionarios xClientes_Funcionarios = new Ilitera.Data.Clientes_Funcionarios();

                Int32 zId = System.Convert.ToInt32(Session["Empresa"].ToString());
                string zD_Inicial = txt_Data.Text.Trim();
                string zD_Final = txt_Data2.Text.Trim();
                string zCliente = Session["NomeEmpresa"].ToString().Trim();


                if (rd_Analitico.Checked == true)
                {
                    Entities.Usuario xUser = (Entities.Usuario)Session["usuarioLogado"];

                    Ilitera.Common.Pessoa zPessoa = new Ilitera.Common.Pessoa();
                    zPessoa.Find(xUser.IdPessoa);

                    Ilitera.Common.Prestador xPrestador = new Ilitera.Common.Prestador();

                    xPrestador = new Ilitera.Common.Prestador();
                    xPrestador.FindByPessoa(zPessoa);
                    xPrestador.IdPessoa.Find();

                    bool rExibir_CID = false;


                    if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)  //.Id == -905238295)  // Capgemini
                    {
                        rExibir_CID = true;
                    }
                    else
                    {
                        if (xPrestador.IndTipoPrestador == (int)Ilitera.Common.TipoPrestador.Medico)
                        {
                            rExibir_CID = true;
                        }
                    }

                    Ilitera.Opsa.Report.DataSourceAbsenteismos dataSource = new Ilitera.Opsa.Report.DataSourceAbsenteismos();
                    zDs = dataSource.GetGuiasAbsenteismo_Acidente( zId, zD_Inicial , zD_Final, zCliente, System.Convert.ToInt16(xEmp), xTipoRel, xConsiderar, xStatus, xAtestados, rExibir_CID);

                    //TextWriter tw = new StreamWriter(myStringWebResource);
                    TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                    
                    //zLinha = "Id;Colaborador;Setor;Funcao;Admissao;Nascimento;Afastamento;Retorno;Tipo;CID1;CID2;CID3;CID4;NumeroCAT;Data1;Data2;Ocorrencias;Dias_Afastado;Obs;Empresa;Afastamento_Data;Afastamento_Hora";
                    zLinha = "Id;Colaborador;Setor;Funcao;Admissao;Nascimento;Afastamento;Retorno;CPF;Empresa;CID1;CID2;CID3;CID4;Atestado;Data1;Data2;Ocorrencias;Dias_Afastado;Tipo;HD;Sab;Data_Afastamento;Hora_Afastamento;Emitentes;Id2;Obs;DataAcidente;Descr_Acidente;Parte_Corpo;Tipo_Local;Descricao_Local;Especificacao_Local";
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
                }
                else if (rd_Sumarizado_Empr.Checked == true)
                {

                    Ilitera.Opsa.Report.DataSourceAbsenteismos dataSource = new Ilitera.Opsa.Report.DataSourceAbsenteismos();
                    zDs = dataSource.GetGuiasAbsenteismo_Empr(zId, zD_Inicial, zD_Final, zCliente, System.Convert.ToInt16(xEmp), xTipoRel, xConsiderar, xStatus, xAtestados);

                    TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));



                    zLinha = "CNPJ;Empregado;Data1;Data2;Nascimento;Admissao;Funcao;Setor;Afastamentos;Jornada;Total_Horas;Horas_Afastadas;Dias;% Absenteismo";
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
                 
                }

                else
                {

                    Ilitera.Opsa.Report.DataSourceAbsenteismos dataSource = new Ilitera.Opsa.Report.DataSourceAbsenteismos();
                    zDs = dataSource.GetGuiasAbsenteismo_Sum(zId, zD_Inicial, zD_Final, zCliente, System.Convert.ToInt16(xEmp), xTipoRel, xConsiderar, xStatus, xAtestados);


                    TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                    zLinha = "CNPJ;Setor;Funcao;;;;;;Cid;;;;;Data1;Data2;Ocorrencias;Total;Empresa";
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
                }

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

                OpenReport("DadosEmpresa", "RelatorioAbsenteismo.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                       + "&IdEmpresa=" + Session["Empresa"].ToString() + "&Data1=" + txt_Data.Text + "&Data2=" + txt_Data2.Text + "&NomeCliente=" + Session["NomeEmpresa"].ToString() + "&xTipo=" + xTipo + "&Emp=" + xEmp + "&TipoRel=" + xTipoRel + "&xConsiderar=" + xConsiderar + "&xStatus=" + xStatus + "&xAtestado=" + xAtestados, "RelatorioAbsenteismo", true);

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
                MsgBox1.Show("Ilitera.Net", "Data Inv�lida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            Validar = zData.Substring(0, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(3, 2);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "M�s Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            Validar = zData.Substring(6, 4);
            isNumerical = int.TryParse(Validar, out myInt);
            if (isNumerical == false)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            if (zData.Substring(2, 1) != "/" || zData.Substring(5, 1) != "/")
            {
                MsgBox1.Show("Ilitera.Net", "Data Inv�lida.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }


            zDia = System.Convert.ToInt32(zData.Substring(0, 2));
            zMes = System.Convert.ToInt32(zData.Substring(3, 2));
            zAno = System.Convert.ToInt32(zData.Substring(6, 4));

            if (zAno < 1900 || zAno > 2025)
            {
                MsgBox1.Show("Ilitera.Net", "Ano Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes < 1 || zMes > 12)
            {
                MsgBox1.Show("Ilitera.Net", "M�s Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                new EO.Web.MsgBoxButton("OK"));
                return false;
            }

            if (zMes == 1 || zMes == 3 || zMes == 5 || zMes == 7 || zMes == 8 || zMes == 10 || zMes == 12)
            {
                if (zDia < 1 || zDia > 31)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else if (zMes == 4 || zMes == 6 || zMes == 9 || zMes == 11)
            {
                if (zDia < 1 || zDia > 30)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }
            else
            {
                if (zDia < 1 || zDia > 29)
                {
                    MsgBox1.Show("Ilitera.Net", "Dia Inv�lido.  Utilizar formato dd/MM/yyyy", null,
                    new EO.Web.MsgBoxButton("OK"));
                    return false;
                }
            }

            return true;

        }

        protected void chk_PDF_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chk_CSV_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rd_Sumarizado_Empr_CheckedChanged(object sender, EventArgs e)
        {

            //if (rd_Analitico.Checked == true) cmb_Empresa.Enabled = false;
            //else cmb_Empresa.Enabled = true;
        }


        
               
	}

}
