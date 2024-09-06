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
using Ilitera.Opsa.Data;
using System.Text;
using Entities;
using System.IO;
using Ilitera.Common;

namespace Ilitera.Net
{
    public partial class CheckPCMSOPeriodos : System.Web.UI.Page
    {


        protected  Entities.Usuario usuario = new Entities.Usuario();
        protected Cliente cliente = new Cliente();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Empresa"] != null && Session["Empresa"].ToString() != String.Empty)
                {
                    //InicializaWebPageObjects(true);

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

                        cliente.Find(System.Convert.ToInt32(Session["Empresa"].ToString()));

                        if (!cliente.ContrataPCMSO.Equals((int)TipoPcmsoContratada.NaoContratada))
                        {
                            Carregar_Prepostos();
                            PopulaDDLPCMSO();
                            PopulaAssinados();
                        }
                        else
                            MsgBox1.Show("PCMSO", "A empresa " + cliente.NomeCompleto + " não possui o PCMSO contratado com a Ilitera!", null,
                                            new EO.Web.MsgBoxButton("OK"));


                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        private void PopulaDDLPCMSO()
        {
            DataSet ds = new DataSet();
            DataRow rowds;

            DataTable table = new DataTable("Default");

            table.Columns.Add("IdPcmso", Type.GetType("System.String"));
            table.Columns.Add("DataPcmso", Type.GetType("System.String"));

            ds.Tables.Add(table);

            DataSet dsaux = new Pcmso().Get("IdCliente=" + Session["Empresa"].ToString()
                + " AND IsFromWeb=0"
                + " ORDER BY DataPcmso DESC");

            foreach (DataRow row in dsaux.Tables[0].Select())
            {
                rowds = ds.Tables[0].NewRow();

                Pcmso pcmso = new Pcmso();
                pcmso.Find(Convert.ToInt32(row["IdPcmso"]));

                rowds["IdPcmso"] = row["IdPcmso"];
                rowds["DataPcmso"] = pcmso.GetPeriodo();

                ds.Tables[0].Rows.Add(rowds);
            }

            ddlPCMSO2.DataSource = ds;
            ddlPCMSO2.DataValueField = "IdPcmso";
            ddlPCMSO2.DataTextField = "DataPcmso";
            ddlPCMSO2.DataBind();

            //ddlPCMSO2.Items.Insert(0, new Infragistics.Web.UI.ListControls.DropDownItem("Selecione...", "0"));

            if (dsaux.Tables[0].Rows.Count > 0)
            {
                ddlPCMSO2.SelectedIndex = 0;
                EventArgs xE = new EventArgs();
                object xSender = new object();

                ddlPCMSO2_SelectedIndexChanged(xSender, xE);
            }
        }

        private void AbrePCMSO(string page)
        {

            Entities.Usuario user = (Entities.Usuario)Session["usuarioLogado"];

            if (ddlPCMSO2.SelectedValue == "0" || ddlPCMSO2.SelectedValue.Trim() == String.Empty)
                MsgBox1.Show("PCMSO", "É necessário selecionar o Período antes de emitir o documento!", null,
                              new EO.Web.MsgBoxButton("OK"));
            else
            {
                Guid strAux = Guid.NewGuid();

                Pcmso pcmso = new Pcmso();
                pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                string xMedicos = "N";

                if (chk_Medicos_Examinadores.Checked == true) xMedicos = "S";

                if (chk_Medicos_Examinadores_Todos.Checked == true) xMedicos = "T";


                string xCronograma = "N";

                if (chk_Inserir_Cronograma.Checked == true) xCronograma = "S";


                string xPreposto = "0";

                if ( chk_Preposto.Checked == true)
                {
                    if ( cmbPreposto.SelectedIndex > 0 )
                    {
                        xPreposto = lst_Id_Preposto.Items[cmbPreposto.SelectedIndex].ToString();
                        pcmso.IdPreposto = System.Convert.ToInt32(xPreposto);
                        pcmso.Save();
                        
                    }
                }

                if (pcmso.IsPCMSOFinalizado())
                    OpenReport("", page + ".aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
                        + "&IdEmpresa=" + Session["Empresa"].ToString() + "&IdPcmso=" + ddlPCMSO2.SelectedValue + "&IdUsuario=" + user.IdUsuario + "&Medicos=" + xMedicos + "&Crono=" + xCronograma + "&Preposto=" + xPreposto, page);
                else
                    MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                              new EO.Web.MsgBoxButton("OK"));

            }
        }

        protected void lkbNormasGerais_Click(object sender, EventArgs e)
        {
            try
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSO");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSO");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }
        protected void lkbPlanilhaGlobal_Click(object sender, EventArgs e)
        {
            try
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {
                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOPlanejamento");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOPlanejamento");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }


        protected void lkbRelatorioAnual_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOAnual");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOAnual");
                }
            
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }
        protected void lkbPlanejEmpreg_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOPorEmpregado");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOPorEmpregado");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }


        protected void lkbPlanejEx_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOAPorExames");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOPorExames");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}')", ex.Message), true);
            }
        }
        protected void lkbExamesRealizados_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOExRealizados");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOExRealizados");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }
        protected void lkbPCMSO_Click(object sender, EventArgs e)
        {
            try
            {

                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOCompleto");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOCompleto");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }

        protected void lkbExamesPlanejados_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOPlanejado");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOPlanejado");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }

        }

        protected void lkbExamesPlanejadosnaoExecutados_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOPlanejado_NE");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOPlanejado_NE");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }





        protected void OpenReport(string directory, string fileAndQuery, string ReportName)
        {
            this.OpenReport(directory, fileAndQuery, ReportName, true);
        }

        protected void OpenReport(string directory, string fileAndQuery, string ReportName, bool useDirectoryForLocalProcess)
        {
            StringBuilder st = new StringBuilder();

            Guid strAux = Guid.NewGuid();

            string valueProcess = "Local";

            if (valueProcess.Equals("Remote"))
                //st.Append("void(window.open('http://report.ilitera.net/index.aspx?Identity=" + strAux.ToString() + "opsa" + strAux.ToString()
                //    + "&Key=" + strAux.ToString() + "MestraNet" + strAux.ToString()
                //    + "&PathAndQuery=" + HttpUtility.UrlEncode(directory + "/" + fileAndQuery) + "', '" + ReportName + "', 'scrollbars=yes, resizable=yes, toolbar=no, addressbar=no, menubar=no, width=800px, height=600px'));");
                System.Diagnostics.Debug.WriteLine(""); 
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

        protected void lkbExamesPlanejadosCSV_Click(object sender, EventArgs e)
        {

            Pcmso pcmso = new Pcmso();
            pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

            if (!pcmso.IsPCMSOFinalizado())
                MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                          new EO.Web.MsgBoxButton("OK"));
            else
            {

                Ilitera.PCMSO.Report.DataSourceExamePlanejado xPlan = new Ilitera.PCMSO.Report.DataSourceExamePlanejado(pcmso, 1);
                DataSet zDs = xPlan.GetDataSource();
                xPlan.GetDataSource(0, zDs);

                string xFile = "Relat_Planejamento_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";


                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "Colaborador;Admissao;Exame;Proximo;Sexo;Nascimento;Idade;Ultimo;Funcao;Setor";
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

            }

            return;


        }

        protected void lkbExamesRealizadosCSV_Click(object sender, EventArgs e)
        {


            Pcmso pcmso = new Pcmso();
            pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

            if (!pcmso.IsPCMSOFinalizado())
                MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                          new EO.Web.MsgBoxButton("OK"));
            else
            {

                Ilitera.PCMSO.Report.DataSourceRelatorioAnualPorEmpregado xPlan = new Ilitera.PCMSO.Report.DataSourceRelatorioAnualPorEmpregado(pcmso);
                DataSet zDs = xPlan.GetDataSouce();

                string xFile = "Relat_Planejamento_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";


                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "GHE;Colaborador;Funcao;Inicio;Termino;Data_Exame;Exame";
                tw.WriteLine(zLinha);

                for (int zCont = 0; zCont < zDs.Tables[0].Rows.Count; zCont++)
                {
                    zLinha = "";

                    for (int zAux = 2; zAux < zDs.Tables[0].Columns.Count; zAux++)
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

            }

            return;

        }

        protected void lkbExamesPlanejadosnaoExecutadosCSV_Click(object sender, EventArgs e)
        {
            Pcmso pcmso = new Pcmso();
            pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

            if (!pcmso.IsPCMSOFinalizado())
                MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                          new EO.Web.MsgBoxButton("OK"));
            else
            {

                Ilitera.PCMSO.Report.DataSourceExamePlanejado xPlan = new Ilitera.PCMSO.Report.DataSourceExamePlanejado(pcmso, 2);
                DataSet zDs = xPlan.GetDataSource();
                xPlan.GetDataSource(0, zDs);

                string xFile = "Relat_Planejamento_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";


                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "Colaborador;Admissao;Exame;Proximo;Sexo;Nascimento;Idade;Ultimo;Funcao;Setor";
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

            }

            return;


        }

        protected void lkbPlanejExCSV_Click(object sender, EventArgs e)
        {
            Pcmso pcmso = new Pcmso();
            pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

            if (!pcmso.IsPCMSOFinalizado())
                MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                          new EO.Web.MsgBoxButton("OK"));
            else
            {

                Ilitera.PCMSO.Report.DataSourceExamePlanejamento xPlan = new Ilitera.PCMSO.Report.DataSourceExamePlanejamento(pcmso);
                DataSet zDs = xPlan.GetDataSource();
                xPlan.GetDataSource(Ilitera.PCMSO.Report.DataSourceExamePlanejamento.TipoRelatorio.PorExame, zDs);


                DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

                zDs.Tables[0].DefaultView.Sort = "NomeExame, NomeEmpregado";

                dsTransf.Tables.Add(zDs.Tables[0].DefaultView.ToTable());


                string xFile = "Relat_Planejamento_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";


                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "DataPcmso;DataAdmissao; Empresa; Colaborador; -; Exame;-;DataVencimento; Sexo; Nascimento;Idade; ExposicaoRisco; -;Ultima";
                tw.WriteLine(zLinha);

                for (int zCont = 0; zCont < dsTransf.Tables[0].Rows.Count; zCont++)
                {
                    zLinha = "";

                    for (int zAux = 3; zAux < dsTransf.Tables[0].Columns.Count - 4; zAux++)
                    {
                        zLinha = zLinha + dsTransf.Tables[0].Rows[zCont][zAux].ToString().Trim() + ";";
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

            }

            return;

        }


        protected void lkbPlanejEmpregCSV_Click(object sender, EventArgs e)
        {

            Pcmso pcmso = new Pcmso();
            pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

            if (!pcmso.IsPCMSOFinalizado())
                MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                          new EO.Web.MsgBoxButton("OK"));
            else
            {

                Ilitera.PCMSO.Report.DataSourceExamePlanejamento xPlan = new Ilitera.PCMSO.Report.DataSourceExamePlanejamento(pcmso);
                DataSet zDs = xPlan.GetDataSource();
                xPlan.GetDataSource(0, zDs);


                DataSet dsTransf = new DataSet(); //Declare a dataSet to be filled.

                zDs.Tables[0].DefaultView.Sort = "NomeEmpregado, NomeExame";

                dsTransf.Tables.Add(zDs.Tables[0].DefaultView.ToTable());

                string xFile = "Relat_Planejamento_" + System.DateTime.Now.Second.ToString().Trim() + System.DateTime.Now.Millisecond.ToString().Trim() + System.DateTime.Now.Minute.ToString().Trim() + System.DateTime.Now.Day.ToString().Trim() + System.DateTime.Now.Month.ToString().Trim() + ".csv";
                string myStringWebResource = "I:\\temp\\" + xFile;
                string zLinha = "";


                TextWriter tw = new StreamWriter(myStringWebResource, false, Encoding.GetEncoding(1252));

                zLinha = "DataPcmso;DataAdmissao; Empresa; Colaborador; -; Exame;-;DataVencimento; Sexo; Nascimento;Idade; ExposicaoRisco; -;Ultima";
                tw.WriteLine(zLinha);

                for (int zCont = 0; zCont < dsTransf.Tables[0].Rows.Count; zCont++)
                {
                    zLinha = "";

                    for (int zAux = 3; zAux < dsTransf.Tables[0].Columns.Count - 4; zAux++)
                    {
                        zLinha = zLinha + dsTransf.Tables[0].Rows[zCont][zAux].ToString().Trim() + ";";
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


            }

            return;


        }

        protected void lkbRelatorioAnualCSV_Click(object sender, EventArgs e)
        {

        }

        protected void lkbPlanilhaGlobalCSV_Click(object sender, EventArgs e)
        {

        }

        protected void lkbNormasGeraisCSV_Click(object sender, EventArgs e)
        {

        }

        protected void lkbPCMSOCSV_Click(object sender, EventArgs e)
        {

        }


        private void Carregar_Prepostos()
        {
            DataSet dsTecCampo = new qryPrestador().Get("IdJuridica=310 AND IsInativo=0 order by NomeAbreviado ");

            lst_Id_Preposto.Items.Clear();
            lst_Id_Preposto.Items.Add("0");

            cmbPreposto.Items.Clear();
            cmbPreposto.Items.Add("  -  ");


            foreach (DataRow row in dsTecCampo.Tables[0].Rows)
            {
                cmbPreposto.Items.Add(Convert.ToString(row["NomeAbreviado"]));
                lst_Id_Preposto.Items.Add(Convert.ToInt32(row["IdPrestador"]).ToString());
            }

            //cmbPreposto.AutoComplete = true;
            cmbPreposto.SelectedIndex = 0;

            return;
        }

        protected void chk_Preposto_CheckedChanged(object sender, EventArgs e)
        {

            //if (chk_Preposto.Checked == true)
            //    cmbPreposto.Enabled = true;
            //else
                cmbPreposto.Enabled = false;

        }

        protected void ddlPCMSO2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if ( ddlPCMSO2.SelectedIndex >= 0 )
            {
                Pcmso pcmso = new Pcmso();
                pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                if ( pcmso.IdPreposto == null || pcmso.IdPreposto == 0)
                {
                    cmbPreposto.SelectedIndex = 0;
                    return;
                }

                for ( int zCont=0;zCont<lst_Id_Preposto.Items.Count;zCont++)
                {
                    if ( lst_Id_Preposto.Items[zCont].ToString().Trim()==pcmso.IdPreposto.ToString().Trim())
                    {
                        cmbPreposto.SelectedIndex = zCont;
                        break;
                    }
                }

            }
            else
            {
                cmbPreposto.SelectedIndex = 0;
            }

           
          
        }

        protected void chk_Medicos_Examinadores_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Medicos_Examinadores.Checked == true)
                chk_Medicos_Examinadores_Todos.Checked = false;
        }

        protected void chk_Medicos_Examinadores_Todos_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Medicos_Examinadores_Todos.Checked == true)
                chk_Medicos_Examinadores.Checked = false;

        }






        private void PopulaAssinados()
        {

            Ilitera.Data.PPRA_EPI xRep = new Ilitera.Data.PPRA_EPI();
            DataSet zDs = xRep.Retornar_Repositorio_Tipo(System.Convert.ToInt32(Session["Empresa"].ToString()), "'M','N'");

            grd_Clinicos.DataSource = zDs;
            grd_Clinicos.DataBind();

        }



        protected void grd_Clinicos_ItemCommand(object sender, EO.Web.GridCommandEventArgs e)
        {


            if (e.CommandName.ToString().Trim() == "3")
            {
                string xArquivo = e.Item.Cells[2].Value.ToString();

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

        protected void lkbPCMSO_Analitico_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ilitera.Data.SQLServer.EntitySQLServer.xDB1.ToString().ToUpper().IndexOf("PRAJNA") > 0)
                {

                    Pcmso pcmso = new Pcmso();
                    pcmso.Find(Convert.ToInt32(ddlPCMSO2.SelectedValue));

                    if (pcmso.IsPCMSOFinalizado())
                        AbrePCMSO("PCMSOAnual");
                    else
                        MsgBox1.Show("PCMSO", "Não é possível visualizar este documento. O planejamento do PCMSO selecionado está em fase de conclusão, sua liberação será automática tão logo seja concluído pelo coordenador médico da Ilitera. Em caso de fiscalização ou qualquer outra necessidade, contate imediatamente o suporte.", null,
                                  new EO.Web.MsgBoxButton("OK"));
                }
                else
                {
                    AbrePCMSO("PCMSOAnalitico");
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", String.Format("alert('{0}');", ex.Message), true);
            }
        }
    }
}
