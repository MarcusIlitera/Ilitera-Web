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

using System.Collections.Generic;

using Ilitera.Opsa.Data;
using Ilitera.Common;
using System.Data.SqlClient;


namespace Ilitera.Net
{
    public partial class CheckPPPEmpregado : System.Web.UI.Page
    {

        private List<EmpregadoFuncao> listEmpregadoFuncao2;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();

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

            PopulaAssinados();


            empregado.Find(System.Convert.ToInt32(Request["IdEmpregado"].ToString()));
            lbl_Colaborador.Text = empregado.tNO_EMPG;

            if (!empregado.Id.Equals(0))
            {

                Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

                try
                {
                    if (empregado.gTERCEIRO)
                        throw new Exception("O documento PPP não pode ser elaborado para este empregado! O empregado é Terceiro, Estagiário ou ainda Não Empregado!");

                    StringBuilder st = new StringBuilder();
                    Guid strAux = Guid.NewGuid();

                    string xPreposto = Session["Preposto"].ToString();

                    //st.Append("function AbrePDF()");
                    //st.Append("{");
                    //st.Append(strOpenReport("Documentos", "PPPEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    //    + "&IdEmpregado=" + empregado.Id + "&IdUsuario=" + user.IdUsuario + "&IdEmpresa=" + cliente.Id + "&Preposto=" + xPreposto, "PPPPDF"));
                    ////st.Append("self.close();");
                    //st.Append("}");

                    //this.ClientScript.RegisterStartupScript(this.GetType(), "OpenPPP", st.ToString(), true);

                    //btnImprimirPPP.Attributes.Add("onClick", "javascript:AbrePDF();");


                    //        btnImprimirPPP.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "PPPEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                    //    +  "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "PPP"));

                    PopulaCheckValues();
                }
                catch (Exception ex)
                {
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                            new EO.Web.MsgBoxButton("OK"));

                }

            }
            grid_Repositorio.DataSource = DSGrid(int.Parse(Request["IdEmpregado"]));
            grid_Repositorio.DataBind();
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
                    //st.AppendFormat("void(window.open('../{0}/{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                    st.AppendFormat("void(window.open('{1}', '{2}','scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", directory, fileAndQuery, ReportName);
                }
                else
                {
                    //st.Append("void(window.open('" + fileAndQuery + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                    st.AppendFormat("void(window.open('{0}','{1}', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));", fileAndQuery, ReportName);
                }
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), String.Format("OpenReport{0}", ReportName), st.ToString(), true);
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


        private void PopulaCheckValues()
        {
            StringBuilder st = new StringBuilder();
            st.Append(empregado.GetStrPPPNullValues());

            ArrayList alemprFuncao = new EmpregadoFuncao().Find("nID_EMPREGADO=" + empregado.Id);

            foreach (EmpregadoFuncao emprFuncao in alemprFuncao)
            {
                emprFuncao.nID_FUNCAO.Find();
                if (emprFuncao.nID_FUNCAO.NumeroCBO == String.Empty)
                    st.Append("CBO da Função '" + emprFuncao.nID_FUNCAO.NomeFuncao + "'\n");
            }

            EmpregadoFuncao empregadoFuncao = EmpregadoFuncao.GetEmpregadoFuncao(empregado);
            ArrayList alEPI = EmpregadoFuncao.GetListaEpiRiscos(empregado);

            foreach (Epi epi in alEPI)
            {
                DataSet ds = new EPIClienteCA().Get("IdEPI=" + epi.Id + " AND IdCliente=" + empregadoFuncao.nID_EMPR.Id);

                if (ds.Tables[0].Rows.Count == 0)
                    st.Append("CA para o EPI '" + epi.ToString() + "'\n");
            }

            //DataSet dsPcmso = new Pcmso().Get("IdCliente=" + cliente.Id);
            //if (dsPcmso.Tables[0].Rows.Count == 0)
            //    st.Append("Coordenador(a) do PCMSO");

            if (st.ToString().Trim() == String.Empty)
                txtFields2.Text = "Todos os dados foram fornecidos!";
            else
                txtFields2.Text = st.ToString();



            if (Checar_GFIP04(empregado) == true)
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().IndexOf("Prajna") > 0)
                {
                    txtFields2.Text = "Não é possível gerar este PPP ( condição 04 ). Contactar Essence centraldeatendimento@essencenet.com.br";
                }
                else
                {
                    txtFields2.Text = "Não é possível gerar este PPP ( condição 04 ). Contactar Ilitera (11) 4249-4949";
                }

                
                btnImprimirPPP.Enabled = false;
                return;
            }

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

        protected void btnImprimirPPP_Click(object sender, EventArgs e)
        {

            //        btnImprimirPPP.Attributes.Add("onClick", "javascript:" + strOpenReport("PCMSO", "PPPEmpregado.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
            //    +  "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&IdUsuario=" + Request["IdUsuario"], "PPP"));

            Guid strAux = Guid.NewGuid();

            string zAssinatura = "N";
            string zRiscos = "N";

            if (chk_Assinatura.Checked == true) zAssinatura = "S";

            if (chk_Riscos.Checked == true) zRiscos = "S";

            OpenReport("DadosEmpresa", "PPPEmpregado.aspx?IliteraSystem=" + strAux.ToString()
           + "&IdEmpresa=" + Request["IdEmpresa"] + "&IdEmpregado=" + Request["IdEmpregado"] + "&Assinatura=" + zAssinatura + "&Riscos=" + zRiscos, "PPP", true);

        }

        protected void cmd_Voltar_Click(object sender, EventArgs e)
        {
            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];
            Response.Redirect("ListaEmpregados.aspx?&IdEmpresa=" + Session["Empresa"].ToString() + "&IdUsuario=" + user.IdUsuario.ToString());
            
        }



        private void PopulaAssinados()
        {

            Ilitera.Data.PPRA_EPI xRep = new Ilitera.Data.PPRA_EPI();
            DataSet zDs = xRep.Retornar_Repositorio_Tipo(System.Convert.ToInt32(Session["Empresa"].ToString()), "'D'"); //, 'X', 'Z'");

            grd_Clinicos.DataSource = zDs;
            grd_Clinicos.DataBind();

        }



        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {


            if (e.CommandName.ToString().Trim() == "4")
            {
                string xArquivo = e.Item.Cells[3].Value.ToString();

                if (xArquivo.ToUpper().IndexOf(".PDF") > 0)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    Byte[] buffer = client.DownloadData(xArquivo);
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + xArquivo);
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                }

            }



        }


        public DataSet DSGrid(Int32 xIdEmpregado)
        {


            DataTable table = new DataTable("Result");
            table.Columns.Add("IdEPI", Type.GetType("System.Int32"));
            table.Columns.Add("EPI", Type.GetType("System.String"));
            table.Columns.Add("Selecao", Type.GetType("System.String"));


            DataSet m_ds = new DataSet();
            m_ds.Tables.Add(table);


            SqlCommand rCommand = new SqlCommand();

            rCommand.Parameters.Add("@xIdEmpregado", SqlDbType.NChar).Value = xIdEmpregado.ToString();

            rCommand.CommandText = " select IdRepositorio, IdCliente, TipoDocumento, DataHora, Descricao, Anexo, "
            + " case when TipoDocumento='V' then 'Vaso de Pressão' when TipoDocumento='C' then 'Caldeira' "
            + "      when TipoDocumento='S' then 'SPDA'            when TipoDocumento='E' then 'Laudo Elétrico' "
            + "      when TipoDocumento='P' then 'Periculosidade ' when TipoDocumento='T' then 'Treinamento' "
            + "      when TipoDocumento='M' then 'PCMSO          ' when TipoDocumento='A' then 'PPRA' "
            + "      when TipoDocumento='L' then 'LTCAT          ' when TipoDocumento='I' then 'Insalubridade' "
            + "      when TipoDocumento='O' then 'Outros'          when TipoDocumento='R' then 'Relat.Gerencial' "
            + "      when TipoDocumento='B' then 'ASO'             when TipoDocumento='Z' then 'PCA' "
            + "      when TipoDocumento='X' then 'PPR'             when TipoDocumento='1' then 'Laudo Ergonômico' "
            + "      when TipoDocumento='D' then 'PPP'             when TipoDocumento='N' then 'Relat.Anual' "
            + "      when TipoDocumento='Y' then 'Banco de Dados'  when TipoDocumento='H' then 'Lista Campanhas' "
            + "      when TipoDocumento='W' then 'PGR'             when TipoDocumento='5' then 'PGRTR'  "
            + "      when TipoDocumento='G' then 'Guia Encaminhamento'   when TipoDocumento='J' then 'COVID'    end as Tipo_Documento  "
            + " from Repositorio"
            + " where IdEmpregado = @xIdEmpregado "
            + " and TipoDocumento = 'D' "
            //if (tipo != "")
            //{
            //    rCommand.Parameters.Add("@xTipoDocumento", SqlDbType.NChar).Value = tipo.ToString();
            //    rCommand.CommandText += " and TipoDocumento = @xTipoDocumento ";
            //}
            //else if (data != null && data.Length >= 2 && data[0] != null && data[1] != null)
            //{
            //    //rCommand.Parameters.AddWithValue("@xDataInicio", data[0] );
            //    //rCommand.Parameters.AddWithValue("@xDataFim",  data[1]);
            //    rCommand.Parameters.Add("@xData", SqlDbType.DateTime).Value = data[0];
            //    rCommand.Parameters.Add("@xDataFim", SqlDbType.DateTime).Value = data[1];
            //    rCommand.CommandText += " and DataHora Between @xData and @xDataFim";
            //}
            //else if (descricao != "")
            //{
            //    rCommand.Parameters.AddWithValue("@xDescricao", "%" + descricao.ToString() + "%");
            //    rCommand.CommandText += " and Descricao Like @xDescricao";
            //}
            //else
            //{

            //}
            + " order by TipoDocumento, DataHora ";


            using (SqlConnection cnn = new SqlConnection(Ilitera.Data.SQLServer.EntitySQLServer.GetConnection()))
            {
                cnn.Open();

                rCommand.Connection = cnn;
                SqlDataAdapter da;


                da = new SqlDataAdapter(rCommand);
                da.Fill(m_ds, "Result");

                //SqlDataAdapter da;
                //cnn.Open();
                //da = new SqlDataAdapter(strSQL.ToString(), cnn);
                //da.Fill(m_ds, "Result");

                cnn.Close();

                da.Dispose();
            }

            return m_ds;

        }



    }
}
