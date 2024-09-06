using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;


//using System.Collections;
using System.Collections.Generic;

using Ilitera.Opsa.Data;

namespace Ilitera.Net.Documentos
{
    public partial class CheckLTCAT : System.Web.UI.Page
    {
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

        private List<EmpregadoFuncao> listEmpregadoFuncao2;

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
                Server.Transfer("~/Tratar_Excecao.aspx");
                return;
            }


            //InicializaWebPageObjects(true);

            //if (!empregado.Id.Equals(0))
            //{


            empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                try
                {

                    StringBuilder st = new StringBuilder();
                    Guid strAux = Guid.NewGuid();

                    st.Append("function AbrePDF()");
                    st.Append("{");
                    st.Append(strOpenReport("Documentos", "LTCAT.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdEmpregado=" + empregado.Id + "&IdUsuario=" + user.IdUsuario + "&IdEmpresa=" + cliente.Id, "PPPPDF"));
                    //st.Append("self.close();");
                    st.Append("}");

                    this.ClientScript.RegisterStartupScript(this.GetType(), "OpenLTCAT", st.ToString(), true);

                    btnImprimirLTCAT.Attributes.Add("onClick", "javascript:AbrePDF();");


                    if (Checar_GFIP04(empregado) == true)
                    {
                        if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                        {
                            lbl_Msg.Text = "Não é possível gerar este LTCAT ( condição 04 ). Contactar Prajna centraldeatendimento@essencenet.com.br";
                        }
                        else
                        {
                            lbl_Msg.Text = "Não é possível gerar este LTCAT ( condição 04 ). Contactar Ilitera (11) 4249-4949";
                        }

                        btnImprimirLTCAT.Enabled = false;
                        return;
                    }
                    


                    //PopulaCheckValues();
                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                           new EO.Web.MsgBoxButton("OK"));    
                }
            //}
        }



    
        private bool Checar_GFIP04(Empregado xEmpr)
        {

            GetEmpregadoFuncao(xEmpr);

            foreach (EmpregadoFuncao emprFunc in listEmpregadoFuncao2)
            {



                LaudoTecnico laudoTecnico = new LaudoTecnico();
                //laudoTecnico.FindMax("hDT_LAUDO", "nID_EMPR=" + cliente.Id + " AND nID_LAUD_TEC IN (SELECT nID_LAUD_TEC FROM " + Mestra.Data.SQLServer.EntitySQLServer.xDB2.ToUpper() + ".dbo.tblFUNC_EMPREGADO)");
                laudoTecnico = emprFunc.GetLaudoTecnico();

                Ghe ghe = new Ghe();

                GheEmpregado gheEmpregado = new GheEmpregado();
                gheEmpregado.Find("nID_LAUD_TEC=" + laudoTecnico.Id + " AND nID_EMPREGADO_FUNCAO=" + emprFunc.Id);

                ghe.Find(gheEmpregado.nID_FUNC.Id);


                if (emprFunc.nIND_GFIP == (int)CodigoGFIP.PPRA)
                {

                    if (ghe.GetGFIP() == "04")
                    {
                        return true;
                    }
                }
                else
                {

                    if (Ghe.GetGFIP(Convert.ToInt32((emprFunc.nIND_GFIP))) == "04")
                        return true;
                }

            }

            return false;

        }


        private List<EmpregadoFuncao> GetEmpregadoFuncao(Empregado zEmpr)
        {

            StringBuilder str = new StringBuilder();

            str.Append("nID_EMPREGADO=" + zEmpr.Id);

            str.Append(" AND nID_EMPREGADO_FUNCAO IN (SELECT nID_EMPREGADO_FUNCAO FROM tblFUNC_EMPREGADO WHERE nID_LAUD_TEC");
            str.Append(" IN (SELECT nID_LAUD_TEC FROM tblLAUDO_TEC WHERE nID_PEDIDO");
            str.Append(" IN (SELECT IdPedido FROM " + Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToUpper() + ".dbo.Pedido WHERE DataConclusao IS NOT NULL) OR bAE=1))");


            //    str.Append(" AND (hDT_TERMINO>='" + dataInicioPPP.ToString("yyyy-MM-dd") + "' OR hDT_TERMINO IS NULL)");

            str.Append(" ORDER BY hDT_INICIO");

            listEmpregadoFuncao2 = new EmpregadoFuncao().Find<EmpregadoFuncao>(str.ToString());




            return listEmpregadoFuncao2;
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
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", directory, fileAndQuery, ReportName);
                }
                else
                {
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px').focus());", fileAndQuery, ReportName);
                }
            }

            return st.ToString();
        }


        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());

        }

   


   }
}
