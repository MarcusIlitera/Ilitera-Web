using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ilitera.Opsa.Data;
using Ilitera.Common;


//using C1.C1Zip;

namespace Ilitera.Net
{
    public partial class Apresentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //InicializaWebPageObjects();
            string xUsuario = Session["usuarioLogado"].ToString();

            if (!IsPostBack)
                PopulaDDLAuditoria();
        }

        private void PopulaDDLAuditoria()
        {
            Cliente cliente;
            cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

            string criteria = String.Format("IdCliente={0}"
                + " AND IdPedido IN (SELECT IdPedido FROM Pedido WHERE IdPedidoGrupo IN"
                + " (SELECT IdPedidoGrupo FROM PedidoGrupo WHERE DataConclusao IS NOT NULL))",
                cliente.Id);

            DataSet dsAuditoria = new Ilitera.Opsa.Data.Auditoria().GetIdNome("DataLevantamento", criteria, "DataLevantamento DESC");

            DataTable tableAuditoria = new DataTable();
            tableAuditoria.Columns.Add("IdAuditoria", typeof(string));
            tableAuditoria.Columns.Add("DataLevantamento", typeof(string));

            DataRow row;

            foreach (DataRow rowAuditoria in dsAuditoria.Tables[0].Select())
            {
                row = tableAuditoria.NewRow();
                row["IdAuditoria"] = rowAuditoria["Id"];
                row["DataLevantamento"] = Convert.ToDateTime(rowAuditoria["Nome"]).ToString("dd-MM-yyyy");
                tableAuditoria.Rows.Add(row);
            }

            ddlAuditoria.DataSource = tableAuditoria;
            ddlAuditoria.DataValueField = "IdAuditoria";
            ddlAuditoria.DataTextField = "DataLevantamento";
            ddlAuditoria.DataBind();
        }

        protected void lkbAuditoriaFlash_Click(object sender, EventArgs e)
        {
            try
            {
                
                Ilitera.Opsa.Data.Auditoria auditoria = new Ilitera.Opsa.Data.Auditoria(Convert.ToInt32(ddlAuditoria.SelectedValue));
                auditoria.PopulaXMLFilesToFlash(Path.Combine(Request.PhysicalApplicationPath, "Auditoria"));

                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                StringBuilder script = new StringBuilder();
                script.Append("void(addItem(centerWin('AuditoriaFlash.aspx?IdUsuario=");
                script.Append(usuario.Id);
                script.Append("&IdEmpresa=");
                script.Append(cliente.Id);
                script.Append("', 779, 523,\'AuditoriaFlash\'),\'Todos\'));");

                this.ClientScript.RegisterStartupScript(this.GetType(), "AuditoriaFlash", script.ToString(), true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message);
            }
        }

        protected void lkbDownload_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime inicio = DateTime.Now;

                Ilitera.Opsa.Data.Auditoria auditoria = new Ilitera.Opsa.Data.Auditoria(Convert.ToInt32(ddlAuditoria.SelectedValue));
                auditoria.PopulaXMLFilesToFlash(Path.Combine(Request.PhysicalApplicationPath, "Auditoria"));

                Cliente cliente;
                cliente = new Cliente(System.Convert.ToInt32(Session["Empresa"].ToString()));

                DirectoryInfo dirClient = new DirectoryInfo(Server.MapPath("ZipFiles/" + cliente.Id));

                if (!dirClient.Exists)
                {
                    dirClient.Create();
                    dirClient.CreateSubdirectory("XML");
                    dirClient.CreateSubdirectory("Fotos");
                }

                FileInfo auditoriaFile = new FileInfo(Server.MapPath("Flash/auditoria.exe"));
                auditoriaFile.CopyTo(Path.Combine(dirClient.FullName, "auditoria.exe"), true);

                FileInfo xmlClientFile = new FileInfo(Server.MapPath("XML/" + cliente.Id + "clientInfo.xml"));
                xmlClientFile.CopyTo(Path.Combine(Path.Combine(dirClient.FullName, "XML"), "clientInfo.xml"), true);

                FileInfo xmlFotoFile = new FileInfo(Server.MapPath("XML/" + cliente.Id + "FotoIrreg.xml"));
                xmlFotoFile.CopyTo(Path.Combine(Path.Combine(dirClient.FullName, "XML"), "FotoIrreg.xml"), true);

                FileInfo xmlIrregularidadeFile = new FileInfo(Server.MapPath("XML/" + cliente.Id + "Irregularidades.xml"));
                xmlIrregularidadeFile.CopyTo(Path.Combine(Path.Combine(dirClient.FullName, "XML"), "Irregularidades.xml"), true);

                FileInfo fotoFile;
                XmlTextReader xmlFotoIrreg = new XmlTextReader(Server.MapPath("ZipFiles/" + cliente.Id + "/XML/FotoIrreg.xml"));

                while (xmlFotoIrreg.Read())
                {
                    if (xmlFotoIrreg.IsStartElement("PathFoto") && !xmlFotoIrreg.IsEmptyElement)
                    {
                        StringBuilder pathFoto = new StringBuilder();
                        pathFoto.Append(Fotos.GetRaizPath());
                        pathFoto.Append(xmlFotoIrreg.ReadString().Substring(12));
                        pathFoto.Replace("/", @"\");

                        fotoFile = new FileInfo(pathFoto.ToString());

                        string destFileName = Path.Combine(Path.Combine(dirClient.FullName, "Fotos"), 
                                            pathFoto.ToString().Substring(pathFoto.ToString().LastIndexOf(@"\") + 1));

                        if(fotoFile.Exists)
                            fotoFile.CopyTo(destFileName, true);
                    }
                }
                xmlFotoIrreg.Close();
                
                //C1ZipFile zipFile = new C1ZipFile(Server.MapPath("ZipFiles/Apresentacao" + cliente.Id + ".zip"), true);
                //zipFile.Entries.AddFolder(dirClient.FullName);
                //zipFile.Close();

                try
                {
                    dirClient.Delete(true);
                }
                catch { }

                TimeSpan timeSpan = DateTime.Now.Subtract(inicio);

                Funcionalidade funcionalidade = Funcionalidade.GetFuncionalidade(FuncionalidadeTipo.Metodo, 
                                                                                 this.GetType(), 
                                                                                 "lkbDownload_Click");

                Usuario usuario = new Usuario();
                usuario.Find(System.Convert.ToInt32(Request["IdUsuario"].ToString()));

                ImpressaoDocUsuario.InsereRegistro(usuario, cliente, timeSpan.Ticks, true, funcionalidade); 

                FileInfo zipFileInfo = new FileInfo(Server.MapPath("ZipFiles/Apresentacao" + cliente.Id + ".zip"));

                Response.AppendHeader("Content-Disposition", "attachment; filename=ApresentacaoAuditoria.zip");
                Response.AppendHeader("Content-Length", zipFileInfo.Length.ToString());
                Response.WriteFile(zipFileInfo.FullName);
                Response.Flush();
                Response.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                //Aviso(ex.Message);
            }
        }
}
}
