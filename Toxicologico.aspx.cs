using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ilitera.Opsa.Data;
using Ilitera.Common;
using Ilitera.Sied.Report;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;


namespace Ilitera.Net
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Toxicologico : System.Web.UI.Page
    {
        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //InicializaWebPageObjects(true);

            string xId = Request["Id"].ToString();
            string xTipo = Request["Tipo"].ToString();

            //pegar ID e passar 
            Int32 zId = System.Convert.ToInt32(xId);


            CrystalDecisions.CrystalReports.Engine.ReportClass report = null;
            report = new Ilitera.Opsa.Report.RptToxicologicoSorteado();
            report.SetDataSource(DataSourceToxicologico(zId, xTipo));
            report.Refresh();

            CreatePDFDocument(report, this.Response);
        }


        private DataSet DataSourceToxicologico(Int32 zId, string xTipo)
        {

            //carregar dados do sorteio
            ToxicologicoSorteio_Colaborador xColab = new ToxicologicoSorteio_Colaborador();
            xColab.Find(zId);

            ToxicologicoSorteio xSorteio = new ToxicologicoSorteio();

            if ( xColab.Id != 0)
            {
                xSorteio.Find( xColab.Id_Toxicologico_Sorteio );
            }


            System.Globalization.CultureInfo ptBr = new System.Globalization.CultureInfo("pt-Br");

            Ilitera.Opsa.Data.Empregado rEmpregado = new Empregado();
            rEmpregado.Find(xColab.IdEmpregado);

            Clinica xClinica = new Clinica();
            xClinica.Find(xSorteio.IdJuridica);

            ExameBase xExame = new ExameBase();
            xExame.Find(" IdExameDicionario = 100 and IdEmpregado = " + rEmpregado.Id.ToString() + " and convert( char(10), DataExame, 103 ) = '" + xColab.Data_Exame.ToString("dd/MM/yyyy", ptBr) + "' ");


            rEmpregado.nID_EMPR.Find();

            

            DataSet ds = new DataSet();

            DataTable table = new DataTable("Result");
            table.Columns.Add("tFraseInicial", Type.GetType("System.String"));

            ds.Tables.Add(table);

            DataRow newRow = ds.Tables[0].NewRow();

            if (xTipo == "1")
            {

                string xCodigo = xExame.CodBusca.ToString().Trim() ;

                if (xCodigo.Length > 9)
                {
                    xCodigo = xCodigo.Substring(xCodigo.Length - 9, 9);
                }
                else
                {
                    xCodigo = new string('0', 9 - xCodigo.Length) + xCodigo;
                }


                newRow["tFraseInicial"] = "Seguindo a Resolução 843 do CONTRAN, informamos que o colaborador " + rEmpregado.tNO_EMPG + ", CPF  " + Convert.ToUInt64(rEmpregado.tNO_CPF).ToString(@"000\.000\.000\-00") + " da empresa " + rEmpregado.nID_EMPR.NomeCompleto + ", de CNPJ " + rEmpregado.nID_EMPR.NomeCodigo + ",   foi sorteado em " + xColab.Data_Sorteio.ToString("dd/MM/yyyy", ptBr) + " e realizou o exame Toxicológico abaixo: " + System.Environment.NewLine + System.Environment.NewLine +
                                          "Código do Sorteio  " + xSorteio.Codigo_Sorteio + System.Environment.NewLine +
                                          "Clínica " + xClinica.NomeCompleto + System.Environment.NewLine +
                                          "CNPJ da Clínica " + xClinica.NomeCodigo + System.Environment.NewLine +
                                          "Data de realização do Exame  " + xColab.Data_Exame.ToString("dd/MM/yyyy", ptBr) + System.Environment.NewLine +
                                          "Código Sequencial do Exame  " + Retornar_Prefixo_Codigo_Sequencial() +  xCodigo;
            }
            else if ( xTipo=="2")
            {
                newRow["tFraseInicial"] = "Seguindo a Resolução 843 do CONTRAN, informamos que o colaborador " + rEmpregado.tNO_EMPG + ", CPF  " + Convert.ToUInt64(rEmpregado.tNO_CPF).ToString(@"000\.000\.000\-00") + " da empresa " + rEmpregado.nID_EMPR.NomeCompleto + ", de CNPJ " + rEmpregado.nID_EMPR.NomeCodigo + ",   participou de sorteio randômico para realização de Exame Toxicológico em em " + xColab.Data_Sorteio.ToString("dd/MM/yyyy", ptBr) + " onde acabou não sendo selecionada." + System.Environment.NewLine + System.Environment.NewLine +
                                          "Código do Sorteio  " + xSorteio.Codigo_Sorteio;
            }



            ds.Tables[0].Rows.Add(newRow);

            return ds;
        }






        protected static void CreatePDFDocument(ReportClass report, HttpResponse response)
        {
            CreatePDFDocument(report, response, false, string.Empty);
        }

        protected static void CreatePDFDocument(ReportClass report, HttpResponse response, bool forceDownload, string DownloadfileName)
        {
            report.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, response, forceDownload, DownloadfileName);
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
                    + "&Key=" + strAux.ToString() + "IliteraNet" + strAux.ToString()
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


        private string Retornar_Prefixo_Codigo_Sequencial()
        {

            string xRet = "";

            if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer == "54.94.157.244")
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() == "OPSA")
                {
                    xRet = "SA";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("DAITI") > 0)
                {
                    xRet = "DT";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("VIA") > 0)
                {
                    xRet = "IL";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("MAPPAS") > 0)
                {
                    xRet = "MP";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRO") > 0)
                {
                    xRet = "PR";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("QTECK") > 0)
                {
                    xRet = "QT";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SAFETY") > 0)
                {
                    xRet = "GR";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("UNO") > 0)
                {
                    xRet = "UN";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SOMA") > 0)
                {
                    xRet = "SM";
                }
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRAJNA_HOM") > 0 || (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().Trim() == "OPSA" && Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("ESSENCE") > 0))
            {
                xRet = "ES";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRAJNA") > 0)
            {
                xRet = "ES";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("161.140") > 0)  //JOHNSON
            {
                xRet = "SC";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("ILITERAEY") > 0)
            {
                xRet = "EY";
            }
            else if (Ilitera.Data.SQLServer.EntitySQLServer._LocalServer.ToUpper().IndexOf("FOCUS") > 0)
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("GLOBAL") > 0)
                {
                    xRet = "GL";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("GOI") > 0)
                {
                    xRet = "GO";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("LIM") > 0)
                {
                    xRet = "LI";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("FOX") > 0)
                {
                    xRet = "FX";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("METODO") > 0)
                {
                    xRet = "MT";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("PRIME") > 0)
                {
                    xRet = "PR";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SAFETY") > 0)
                {
                    xRet = "SF";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SHO") > 0)
                {
                    xRet = "ER";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper().IndexOf("SDTOURIN") > 0)
                {
                    xRet = "ST";
                }
                else if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() == "OPSA")
                {
                    xRet = "OP";
                }

            }


            return xRet;


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
    }
}
