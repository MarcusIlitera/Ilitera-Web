using System;
using System.Data;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Text;
using Ilitera.Opsa.Data;


namespace Ilitera.Net.OrdemDeServico
{
	/// <summary>
	/// Summary description for ListaEquipamento.
	/// </summary>
	public partial class ImportProcGenerico : System.Web.UI.Page
	{

		public void PageLoadEvent(object sender, System.EventArgs e)
		{
            string xUsuario = Session["usuarioLogado"].ToString();
            //InicializaWebPageObjects();
            if (!IsPostBack)
				PopulaLsbProcedimentoG();
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
			this.Load += new System.EventHandler(this.PageLoadEvent);

		}
		#endregion

		private void PopulaLsbProcedimentoG()
		{
			//lsbProcedGenericos.DataSource = new Procedimento().Get("isGenerico=1 AND IdCliente IS NULL ORDER BY Nome");
            //lsbProcedGenericos.DataSource = new Procedimento().Get(" ( isGenerico = 0 or isGenerico is null ) AND IdCliente <> " + Session["Empresa"].ToString() + " ORDER BY Nome");
            lsbProcedGenericos.DataSource = new Procedimento().Get(" ( isGenerico = 0 or isGenerico is null ) AND IdCliente <> " + Session["Empresa"].ToString() + " and Nome not in ( Select Nome from procedimento where IdCliente = " + Session["Empresa"].ToString() + "  ) ORDER BY Nome");
			lsbProcedGenericos.DataValueField = "IdProcedimento";
			lsbProcedGenericos.DataTextField = "Nome";
			lsbProcedGenericos.DataBind();
		}

		protected void btnImportar_Click(object sender, System.EventArgs e)
		{
			if (lsbProcedGenericos.SelectedItem != null)
				try
				{
					string newProcedIds = string.Empty;

					foreach (ListItem procedimento in lsbProcedGenericos.Items)
						if (procedimento.Selected)
						{
							Procedimento procedGenerico = new Procedimento(Convert.ToInt32(procedimento.Value));

                            Procedimento newProcedGenerico = procedGenerico.GetNewProcedimento(System.Convert.ToInt32(Session["IdUsuario"]), System.Convert.ToInt32(Session["Empresa"]));

							if (newProcedIds.Equals(string.Empty))
								newProcedIds = newProcedGenerico.Numero.ToString("0000");
							else
								newProcedIds += "; " + newProcedGenerico.Numero.ToString("0000");
						}

                    if (!newProcedIds.Equals(string.Empty))
                    {

                        MsgBox1.Show("Ilitera.Net", "Os Procedimentos Genéricos selecionados foram importados com sucesso! Os Novos nPOPs adicionados são: " + newProcedIds, null,
                                               new EO.Web.MsgBoxButton("OK"));
                        //Response.Write("<script>window.opener.document.forms[0].submit(); window.close();</script>");
                    }
				}
				catch (Exception ex)
				{
                    MsgBox1.Show("Ilitera.Net", ex.Message, null,
                       new EO.Web.MsgBoxButton("OK"));
				}
			else
                MsgBox1.Show("Ilitera.Net", "É necessário selecionar pelo menos 1 Procedimento Genérico!", null,
                       new EO.Web.MsgBoxButton("OK"));
		}
	}
}
