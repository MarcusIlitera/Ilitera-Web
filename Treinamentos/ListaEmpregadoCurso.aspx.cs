using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using Ilitera.Common;
using Ilitera.Opsa.Data;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;


namespace Ilitera.Net.Treinamentos
{
	/// <summary>
	/// Summary description for ListaEmpresa.
	/// </summary>
	public partial class ListaEmpregadoCurso : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblEmpresa;
		protected System.Web.UI.WebControls.LinkButton lbtSelTodos;

        //private Treinamento treinamento;
        //private Infragistics.WebUI.WebDataInput.WebDateTimeEdit wdeDataInicio;

        protected Prestador prestador = new Prestador();
        protected Usuario usuario = new Usuario();
        protected Cliente cliente = new Cliente();
        protected Empregado empregado = new Empregado();
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//InicializaWebPageObjects();
			if (!Page.IsPostBack)
			{
				PopulaTitle();
				PopulaListaEmpregadoCurso(GetParticipantes());
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

		private void PopulaTitle()
		{
			Treinamento treinamento = new Treinamento(Convert.ToInt32(Request["IdTreinamento"]));
			treinamento.IdTreinamentoDicionario.Find();
			
			if (treinamento.IsFromCliente)
				lblCurso.Text = "Participantes do '" + treinamento.IdTreinamentoDicionario.Nome + "'";
			else
			{
				treinamento.IdTreinamentoDicionario.IdObrigacao.Find();

				lblCurso.Text = "Participantes do '" + treinamento.IdTreinamentoDicionario.IdObrigacao.NomeReduzido + "'";
			}
		}

		private DataSet GetParticipantes()
		{
			ArrayList alParticipantes = new ParticipanteTreinamento().Find("IdTreinamento=" + Request["IdTreinamento"]);

			DataSet dsSortedEmpregado = new DataSet(), ds = new DataSet();
			DataTable table = new DataTable(), sortedtable = new DataTable();
			DataRow row, sortedRow;

			table.Columns.Add("IdParticipante", typeof(string));
			table.Columns.Add("NomeParticipante", typeof(string));
			ds.Tables.Add(table);

			sortedtable.Columns.Add("IdParticipante", typeof(string));
			sortedtable.Columns.Add("NomeParticipante", typeof(string));
			dsSortedEmpregado.Tables.Add(sortedtable);

			foreach (ParticipanteTreinamento participante in alParticipantes)
			{
				row = ds.Tables[0].NewRow();
				row["IdParticipante"] = participante.Id.ToString();
				
				if (participante.IdEmpregado.Id.Equals(0))
					row["NomeParticipante"] = participante.NomeParticipante;
				else
				{
					participante.IdEmpregado.Find();
					row["NomeParticipante"] = participante.IdEmpregado.tNO_EMPG;
				}

				ds.Tables[0].Rows.Add(row);
			}

			DataRow[] rows = ds.Tables[0].Select("", "NomeParticipante");

			foreach (DataRow Row in rows)
			{
				sortedRow = dsSortedEmpregado.Tables[0].NewRow();
				sortedRow["IdParticipante"] = Row["IdParticipante"];
				sortedRow["NomeParticipante"] = Row["NomeParticipante"];
				dsSortedEmpregado.Tables[0].Rows.Add(sortedRow);
			}

			return dsSortedEmpregado;
		}

		private void PopulaListaEmpregadoCurso(DataSet empregados)
		{
			ltbEmpregadoCurso.DataSource = empregados;
			ltbEmpregadoCurso.DataTextField = "NomeParticipante";
			ltbEmpregadoCurso.DataValueField = "IdParticipante";
			ltbEmpregadoCurso.DataBind();
		}

		private string IdsParticipanteSelecionado()
		{
			string IdsParticipantes = string.Empty;

			foreach (ListItem item in ltbEmpregadoCurso.Items)
				if (item.Selected)
				{
					if (IdsParticipantes.Equals(string.Empty))
						IdsParticipantes = item.Value;
					else
						IdsParticipantes = IdsParticipantes + ", " + item.Value;
				}

			return IdsParticipantes;
		}

		protected void lkbListagemParticipantes_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			if (ltbEmpregadoCurso.Items.Count > 0)
				OpenReport("Treinamentos", "ParticipantesCurso.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString() + "&IdEmpresa=" + cliente.Id + "&IdUsuario=" + usuario.Id
					+ "&IdTreinamento=" + Request["IdTreinamento"], "ParticipanteTreinamento");
			else
                MsgBox1.Show("Ilitera.Net", "Não há nenhum participante para este Treinamento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

		}

		protected void lkbColetivo_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();

			if (ltbEmpregadoCurso.Items.Count > 0)
                OpenReport("Treinamentos", "CertificadoColetivo.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
					+ "&IdTreinamento=" + Request["IdTreinamento"] + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id, "CertificadoColetivo");
			else
                MsgBox1.Show("Ilitera.Net", "Não há nenhum participante para este Treinamento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

		}

		protected void lkbIndividual_Click(object sender, System.EventArgs e)
		{
			Guid strAux = Guid.NewGuid();
		
			if (ltbEmpregadoCurso.Items.Count > 0)
				OpenReport("Treinamentos", "CertificadoIndividual.aspx?IliteraSystem=" + strAux.ToString() + strAux.ToString()
					+ "&Participantes=" + IdsParticipanteSelecionado() + "&IdTreinamento=" + Request["IdTreinamento"] + "&IdUsuario=" + usuario.Id + "&IdEmpresa=" + cliente.Id, "CertificadoIndividual");
			else
                MsgBox1.Show("Ilitera.Net", "Não há nenhum participante para este Treinamento!", null,
                            new EO.Web.MsgBoxButton("OK"));                

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


	}
}
